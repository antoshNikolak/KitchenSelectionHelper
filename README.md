# Customer Management

Minimal full-stack sample:

- `backend`: ASP.NET Core API (`/api/customers`)
- `front_end`: Vite + TypeScript UI that renders customer names

## Quick start

```bash
cd /home/antoni-nikolak/WebstormProjects/customer-management/backend
dotnet run
```

In another terminal:

```bash
cd /home/antoni-nikolak/WebstormProjects/customer-management/front_end
cp .env.example .env
npm install
npm run dev
```

Open the URL printed by Vite (typically `http://localhost:5173`).

## Notes

- Frontend calls `GET /api/customers`.
- In development, Vite proxies `/api` to `BACKEND_URL` from `.env`.
- Default backend URL is `http://localhost:5238`.

