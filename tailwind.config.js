/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./Views/**/*.{html,js,razor,cshtml}"],
  theme: {
    extend: {},
  },
  plugins: [require("daisyui")],
  daisyui: {
    themes: ["light"],
  },
}

