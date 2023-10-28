module.exports = {
    content: [
        './Pages/**/*.cshtml',
        './Views/**/*.chstml',
        "./node_modules/flowbite/**/*.js"
    ],
    theme: {
        extend: {},
    },
    plugins: [
        require('flowbite/plugin')
    ],
}