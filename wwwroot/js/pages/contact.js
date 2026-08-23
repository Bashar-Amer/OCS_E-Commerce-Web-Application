/**
 * PAGE SCRIPT: CONTACT US
 */

document.addEventListener('DOMContentLoaded', () => {

    const form = document.getElementById('testimonialForm');
    const modalElement = document.getElementById('loginRequiredModal');
    const loginButton = document.getElementById('loginRequiredBtn');

    if (!form) {
        return;
    }

    // =========================================
    // CHECK LOGIN STATUS
    // =========================================

    const isLoggedIn =
        form.dataset.loggedIn === 'True' ||
        form.dataset.loggedIn === 'true';


    // =========================================
    // FORM ELEMENTS
    // =========================================

    const nameInput =
        document.getElementById('testimonialName');

    const contentInput =
        document.getElementById('testimonialContent');


    // =========================================
    // RESTORE DATA AFTER LOGIN
    // =========================================

    const pendingName =
        sessionStorage.getItem('pendingTestimonialName');

    const pendingContent =
        sessionStorage.getItem('pendingTestimonialContent');

    if (pendingName && nameInput) {
        nameInput.value = pendingName;
    }

    if (pendingContent && contentInput) {
        contentInput.value = pendingContent;
    }


    // =========================================
    // SUBMIT
    // =========================================

    form.addEventListener('submit', (e) => {

        // User is NOT logged in
        if (!isLoggedIn) {

            e.preventDefault();

            // Save entered data temporarily
            sessionStorage.setItem(
                'pendingTestimonialName',
                nameInput.value
            );

            sessionStorage.setItem(
                'pendingTestimonialContent',
                contentInput.value
            );


            // Show Login Required modal
            if (
                modalElement &&
                typeof bootstrap !== 'undefined'
            ) {

                const modal =
                    bootstrap.Modal.getOrCreateInstance(
                        modalElement
                    );

                modal.show();
            }

            return;
        }


        // =========================================
        // USER IS LOGGED IN
        // =========================================

        sessionStorage.removeItem(
            'pendingTestimonialName'
        );

        sessionStorage.removeItem(
            'pendingTestimonialContent'
        );

        // Allow normal form submission
    });


    // =========================================
    // LOGIN BUTTON
    // =========================================

    if (loginButton) {

        loginButton.addEventListener('click', () => {

            const returnUrl =
                window.location.pathname +
                window.location.search;

            window.location.href =
                '/Identity/Account/Login?returnUrl=' +
                encodeURIComponent(returnUrl);

        });

    }

});