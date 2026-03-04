/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./Pages/**/*.razor",
    "./Layout/**/*.razor",
    "./Features/**/*.razor",
    "./Shared/**/*.razor",
    "./App.razor",
    "./wwwroot/index.html"
  ],
  theme: {
    extend: {
      fontFamily: {
        myanmar: ['"Pyidaungsu"', '"Myanmar3"', 'serif'],
      },
    },
  },
  plugins: [],
}
