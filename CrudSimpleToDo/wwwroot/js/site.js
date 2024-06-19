
// for details on configuring this project to bundle and minify static web assets.

// site.js
document.addEventListener('DOMContentLoaded', function () {
    console.log("Document loaded. Adding event listeners...");

    document.querySelectorAll('.done-button').forEach(button => {
        button.addEventListener('mouseenter', function () {
            console.log("Mouse entered button.");
            // Show the warning message
            document.getElementById('warningMessage').style.display = 'block';
            // Position the warning message next to the button
            const buttonRect = button.getBoundingClientRect();
            const warningMessage = document.getElementById('warningMessage');
            warningMessage.style.top = buttonRect.top + buttonRect.height + 'px';
            warningMessage.style.left = buttonRect.left + 'px';
        });

        button.addEventListener('mouseleave', function () {
            console.log("Mouse left button.");
            // Hide the warning message
            document.getElementById('warningMessage').style.display = 'none';
        });
    });
});