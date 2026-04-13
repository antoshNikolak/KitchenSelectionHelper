import { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';

export default function Quote() {
  const navigate = useNavigate();
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [submitError, setSubmitError] = useState('');
  const [formData, setFormData] = useState({
    kitchenSize: '',
    installationByMagnet: null,
    wallArea: '',
    hasDishwasher: null,
    totalAppliances: '',
    sockets: '',
  });

  const needsExtraQuestions = formData.installationByMagnet === false;

  const isComplete =
    formData.kitchenSize.trim() !== '' &&
    formData.installationByMagnet !== null &&
    formData.wallArea.trim() !== '' &&
    (!needsExtraQuestions ||
      (formData.hasDishwasher !== null &&
        formData.totalAppliances.trim() !== '' &&
        formData.sockets.trim() !== ''));

  function handleChange(event) {
    const { name, value } = event.target;
    const booleanFields = ['installationByMagnet', 'hasDishwasher'];
    const parsedValue = booleanFields.includes(name) ? value === 'true' : value;

    setFormData((current) => {
      const next = { ...current, [name]: parsedValue };

      // Clear hidden follow-up answers when installation switches back to Yes.
      if (name === 'installationByMagnet' && parsedValue === true) {
        next.hasDishwasher = null;
        next.totalAppliances = '';
        next.sockets = '';
      }

      return next;
    });
  }

  async function handleSubmit(event) {
    event.preventDefault();
    if (!isComplete) {
      return;
    }

    const payload = {
      kitchenSize: Number(formData.kitchenSize),
      installationByMagnet: formData.installationByMagnet,
      wallArea: Number(formData.wallArea),
      hasDishwasher: needsExtraQuestions ? formData.hasDishwasher : null,
      totalAppliances: needsExtraQuestions
        ? Number(formData.totalAppliances)
        : null,
      sockets: needsExtraQuestions ? Number(formData.sockets) : null,
    };

    setSubmitError('');
    setIsSubmitting(true);

    try {
      const response = await fetch('/api/quote/calculate', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(payload),
      });

      if (!response.ok) {
        throw new Error(`Calculation failed with status ${response.status}`);
      }

      const expenses = await response.json();
      navigate('/results', { state: { expenses } });
    } catch {
      setSubmitError('Could not calculate quote. Please try again.');
    } finally {
      setIsSubmitting(false);
    }
  }

  return (
    <main className="flex min-h-screen items-center justify-center bg-slate-950 px-4 py-8 sm:px-8">
      <section className="w-full max-w-2xl rounded-3xl border border-white/10 bg-slate-900/85 p-6 shadow-2xl sm:p-8">
        <p className="mb-3 text-sm font-medium uppercase tracking-[0.08em] text-sky-300">
          Step 1 of 2
        </p>
        <h1 className="text-3xl font-semibold text-white">Build your quote</h1>
        <p className="mt-3 text-slate-300">
          Answer the questions below and continue to results.
        </p>

        <form className="mt-6 space-y-5" onSubmit={handleSubmit}>
          {submitError && (
            <p className="rounded-xl border border-red-400/35 bg-red-500/10 px-3 py-2 text-sm text-red-200">
              {submitError}
            </p>
          )}

          <label className="block text-sm font-medium text-slate-200">
            1. What is your kitchen size (m^2)?
            <input
              className="mt-2 w-full rounded-xl border border-white/15 bg-slate-950 px-3 py-2.5 text-slate-100 outline-none transition focus:border-sky-400"
              name="kitchenSize"
              type="number"
              min="1"
              value={formData.kitchenSize}
              onChange={handleChange}
              placeholder="e.g. 18"
              required
            />
          </label>

          <label className="block text-sm font-medium text-slate-200">
            3. What is the area of the wall (m^2)?
            <input
              className="mt-2 w-full rounded-xl border border-white/15 bg-slate-950 px-3 py-2.5 text-slate-100 outline-none transition focus:border-sky-400"
              name="wallArea"
              type="number"
              min="1"
              value={formData.wallArea}
              onChange={handleChange}
              placeholder="e.g. 22"
              required
            />
          </label>

          <label className="block text-sm font-medium text-slate-200">
            2. Is Magnet doing your kitchen installation?
            <div className="mt-2 space-y-2">
              <label className="flex items-center gap-2 text-slate-200">
                <input
                  type="radio"
                  name="installationByMagnet"
                  value="true"
                  checked={formData.installationByMagnet === true}
                  onChange={handleChange}
                />
                Yes
              </label>

              <label className="flex items-center gap-2 text-slate-200">
                <input
                  type="radio"
                  name="installationByMagnet"
                  value="false"
                  checked={formData.installationByMagnet === false}
                  onChange={handleChange}
                />
                No
              </label>
            </div>
          </label>

          {needsExtraQuestions && (
            <>
              <label className="block text-sm font-medium text-slate-200">
                4. Do you have a dishwasher?
                <div className="mt-2 space-y-2">
                  <label className="flex items-center gap-2 text-slate-200">
                    <input
                      type="radio"
                      name="hasDishwasher"
                      value="true"
                      checked={formData.hasDishwasher === true}
                      onChange={handleChange}
                    />
                    Yes
                  </label>

                  <label className="flex items-center gap-2 text-slate-200">
                    <input
                      type="radio"
                      name="hasDishwasher"
                      value="false"
                      checked={formData.hasDishwasher === false}
                      onChange={handleChange}
                    />
                    No
                  </label>
                </div>
              </label>

              <label className="block text-sm font-medium text-slate-200">
                5. How many appliances do you have in total?
                <input
                  className="mt-2 w-full rounded-xl border border-white/15 bg-slate-950 px-3 py-2.5 text-slate-100 outline-none transition focus:border-sky-400"
                  name="totalAppliances"
                  type="number"
                  min="0"
                  value={formData.totalAppliances}
                  onChange={handleChange}
                  placeholder="e.g. 6"
                  required={needsExtraQuestions}
                />
              </label>

              <label className="block text-sm font-medium text-slate-200">
                6. How many sockets do you want?
                <input
                  className="mt-2 w-full rounded-xl border border-white/15 bg-slate-950 px-3 py-2.5 text-slate-100 outline-none transition focus:border-sky-400"
                  name="sockets"
                  type="number"
                  min="0"
                  value={formData.sockets}
                  onChange={handleChange}
                  placeholder="e.g. 8"
                  required={needsExtraQuestions}
                />
              </label>
            </>
          )}

          <div className="flex flex-col gap-3 pt-2 sm:flex-row sm:justify-between">
            <Link
              to="/"
              className="inline-flex items-center justify-center rounded-xl border border-white/15 px-4 py-2.5 font-medium text-slate-200 transition hover:bg-white/5"
            >
              Back
            </Link>
            <button
              type="submit"
              disabled={!isComplete || isSubmitting}
              className="inline-flex items-center justify-center rounded-xl bg-sky-400 px-5 py-2.5 font-semibold text-slate-950 transition hover:bg-sky-300 disabled:cursor-not-allowed disabled:bg-slate-500"
            >
              {isSubmitting ? 'Calculating...' : 'Complete'}
            </button>
          </div>
        </form>
      </section>
    </main>
  );
}
