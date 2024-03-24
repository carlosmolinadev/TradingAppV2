// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.body.addEventListener('htmx:configRequest', function (event) {
    if (event.detail.parameters) {
        for (let key in event.detail.parameters) {
            if (event.detail.parameters[key] === "") {
                delete event.detail.parameters[key];
            }
        }
    }
});