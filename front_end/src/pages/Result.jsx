import { Link, useLocation } from 'react-router-dom';

export default function Result() {
  const { state } = useLocation();

  const expenses = Object.entries(state.expenses);

  let total = 0;
  for (const [key, value] of expenses) {
    total += value;
  }

  return (
    <main className="flex min-h-screen items-center justify-center bg-slate-950 px-4 py-8 sm:px-8">
      <section className="w-full max-w-2xl rounded-3xl border border-white/10 bg-slate-900/85 p-6 shadow-2xl sm:p-8">
        <p className="mb-3 text-sm font-medium uppercase tracking-[0.08em] text-sky-300">
          Step 2 of 2
        </p>
        <h1 className="text-3xl font-semibold text-white">
          Your quote results
        </h1>

        <div className="mt-6 overflow-hidden rounded-2xl border border-white/10">
          <ul className="divide-y divide-white/10 bg-slate-950/40">
            {expenses.map(([type, amount]) => (
              <li
                key={type}
                className="flex items-center justify-between px-4 py-3 text-slate-100"
              >
                <span>{type}</span>
                <span className="font-medium">{'£' + amount}</span>
              </li>
            ))}
          </ul>
        </div>

        <div className="mt-5 flex items-center justify-between rounded-2xl bg-slate-950 px-4 py-3 text-slate-100">
          <span className="text-sm uppercase tracking-wide text-slate-300">
            Total
          </span>
          <span className="text-xl font-semibold text-sky-300">
            {'£' + total}
          </span>
        </div>

        <div className="mt-6 flex flex-col gap-3 sm:flex-row sm:justify-between">
          <Link
            to="/quote"
            className="inline-flex items-center justify-center rounded-xl border border-white/15 px-4 py-2.5 font-medium text-slate-200 transition hover:bg-white/5"
          >
            Edit answers
          </Link>
          <Link
            to="/"
            className="inline-flex items-center justify-center rounded-xl bg-sky-400 px-5 py-2.5 font-semibold text-slate-950 transition hover:bg-sky-300"
          >
            Back home
          </Link>
        </div>
      </section>
    </main>
  );
}
