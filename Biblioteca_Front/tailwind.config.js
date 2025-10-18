/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./src/**/*.{html,ts}"],
  theme: {
    extend: {
      colors: {
        primary: '#10B981',
        primaryDark: '#059669',
        secondary: '#065F46',
        background: '#F0FDF4',
        textMain: '#064E3B',
        accent: '#F59E0B',
      },
      fontFamily: {
        sans: ['"Quicksand"', 'sans-serif'],
      },
    },
  },
  plugins: [],
};
