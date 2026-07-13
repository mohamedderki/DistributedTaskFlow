import "../styles/globals.css";

export const metadata = {
  title: "TaskFlow",
  description: "DistributedTaskFlow frontend",
};

export default function RootLayout({ children }) {
  return (
    <html lang="en">
      <body>{children}</body>
    </html>
  );
}
