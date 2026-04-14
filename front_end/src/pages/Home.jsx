import { Link } from 'react-router-dom';

const highlights = [
  {
    title: 'Hidden cost breakdown',
    description:
      'Break down materials, labour, electrics, and plumbing clearly.',
  },
  {
    title: 'Customer clarity',
    description: 'Help users understand what’s included vs extra costs.',
  },
];

export default function Home() {
  return (
    <main className="flex min-h-screen items-center justify-center bg-[radial-gradient(circle_at_top_left,rgba(79,70,229,0.28),transparent_32%),radial-gradient(circle_at_bottom_right,rgba(14,165,233,0.25),transparent_28%),linear-gradient(135deg,#081120_0%,#0f1b33_45%,#15284d_100%)] px-4 py-8 sm:px-8">
      <section className="relative w-full max-w-245 overflow-hidden rounded-4xl border border-white/14 bg-[rgba(8,18,35,0.72)] p-6 shadow-[0_24px_70px_rgba(0,0,0,0.35)] backdrop-blur-md sm:p-10 lg:p-14">
        <div className="pointer-events-none absolute inset-0 bg-linear-to-br from-sky-400/10 via-transparent to-indigo-400/10" />

        <div className="relative z-10">
          <p className="mb-5 inline-block rounded-full border border-sky-300/35 bg-sky-400/12 px-3.5 py-2 text-[13px] uppercase tracking-[0.08em] text-sky-200">
            Hidden cost estimator • UK kitchen projects
          </p>

          <h1 className="max-w-4xl text-[clamp(2.8rem,6vw,4.8rem)] leading-[1.02] font-semibold text-white">
            Kitchen Hidden Cost Calculator
          </h1>

          <p className="mt-5 max-w-160 text-[1.1rem] leading-[1.8] text-slate-200/82">
            Estimate the true cost of a kitchen project by breaking down
            materials, installation, plumbing, electrics, and unexpected extras.
          </p>

          <div className="mt-8 flex flex-col items-stretch gap-4 sm:flex-row sm:flex-wrap">
            <Link
              to="/quote"
              className="inline-flex min-w-42.5 items-center justify-center rounded-[14px] bg-linear-to-br from-sky-300 to-sky-400 px-5.5 py-3.5 font-bold text-slate-950 shadow-[0_12px_24px_rgba(56,189,248,0.28)] transition duration-200 hover:-translate-y-0.5 hover:shadow-[0_16px_28px_rgba(56,189,248,0.34)]"
            >
              Calculate hidden costs
            </Link>

          </div>

          <div className="mt-10 grid gap-4.5 md:grid-cols-3">
            {highlights.map((highlight) => (
              <article
                key={highlight.title}
                className="flex flex-col gap-2.5 rounded-[20px] border border-white/8 bg-white/5 p-5.5"
              >
                <strong className="text-base text-white">
                  {highlight.title}
                </strong>
                <span className="leading-6 text-slate-200/78">
                  {highlight.description}
                </span>
              </article>
            ))}
          </div>
        </div>
      </section>
    </main>
  );
}
