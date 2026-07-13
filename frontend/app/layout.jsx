import "../styles/globals.css";

export const metadata = {
  title: "TaskFlow Dashboard",
  description: "Responsive frontend for the DistributedTaskFlow task dashboard.",
};

export default function RootLayout({ children }) {
  return (
    <html lang="en">
      {/*
        suppressHydrationWarning only affects <body>'s own attributes (one level
        deep). Browser extensions such as ColorZilla inject attributes like
        cz-shortcut-listen onto <body> before React hydrates, which otherwise
        triggers a false hydration mismatch. Real hydration errors in children
        are still reported.
      */}
      <body suppressHydrationWarning>{children}</body>
    </html>
  );
}
