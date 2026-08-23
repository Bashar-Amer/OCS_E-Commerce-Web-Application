/**
 * PAGE SCRIPT: PRODUCT DETAILS (Details.cshtml)
 */

document.addEventListener('DOMContentLoaded', () => {

    // =========================================================
    // Thumbnail switching
    // =========================================================

    document.querySelectorAll('.detail-thumb').forEach(img => {

        img.addEventListener('click', function () {

            const mainImg = document.getElementById('mainDetailImg');

            if (!mainImg) return;

            // Use the actual thumbnail image URL
            mainImg.src = this.src;

            // Update active thumbnail
            document.querySelectorAll('.detail-thumb')
                .forEach(i => {
                    i.classList.remove('detail-thumb-active');
                });

            this.classList.add('detail-thumb-active');
        });

    });

});