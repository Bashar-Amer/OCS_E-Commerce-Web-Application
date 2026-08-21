/**
 * BARRAMERU ADMIN: REVIEWS & FEEDBACK SCRIPT (reviews.js)
 * Live search filter and SweetAlert2 moderation confirmation dialogs
 */

document.addEventListener('DOMContentLoaded', () => {
    // 1. Search Filter for Reviews
    const reviewSearchInput = document.getElementById('reviewSearchInput');
    const reviewsTableBody = document.getElementById('reviewsTableBody');

    if (reviewSearchInput && reviewsTableBody) {
        reviewSearchInput.addEventListener('input', () => {
            const query = reviewSearchInput.value.toLowerCase().trim();
            const rows = reviewsTableBody.querySelectorAll('tr.review-row');
            let visibleCount = 0;

            rows.forEach(row => {
                const productText = row.querySelector('.review-product-cell')?.textContent.toLowerCase() || '';
                const customerText = row.querySelector('.review-customer-cell')?.textContent.toLowerCase() || '';
                const commentText = row.querySelector('.review-comment-cell')?.textContent.toLowerCase() || '';

                if (productText.includes(query) || customerText.includes(query) || commentText.includes(query)) {
                    row.style.display = '';
                    visibleCount++;
                } else {
                    row.style.display = 'none';
                }
            });

            const emptyRow = document.getElementById('noReviewMatchRow');
            if (emptyRow) {
                emptyRow.style.display = visibleCount === 0 ? '' : 'none';
            }
        });
    }

    // 2. Search Filter for Testimonials
    const testimonialSearchInput = document.getElementById('testimonialSearchInput');
    const testimonialsTableBody = document.getElementById('testimonialsTableBody');

    if (testimonialSearchInput && testimonialsTableBody) {
        testimonialSearchInput.addEventListener('input', () => {
            const query = testimonialSearchInput.value.toLowerCase().trim();
            const rows = testimonialsTableBody.querySelectorAll('tr.testimonial-row');
            let visibleCount = 0;

            rows.forEach(row => {
                const authorText = row.querySelector('.testimonial-author-cell')?.textContent.toLowerCase() || '';
                const contentText = row.querySelector('.testimonial-content-cell')?.textContent.toLowerCase() || '';

                if (authorText.includes(query) || contentText.includes(query)) {
                    row.style.display = '';
                    visibleCount++;
                } else {
                    row.style.display = 'none';
                }
            });

            const emptyRow = document.getElementById('noTestimonialMatchRow');
            if (emptyRow) {
                emptyRow.style.display = visibleCount === 0 ? '' : 'none';
            }
        });
    }
});

/**
 * Reusable Confirmation for Moderation (Accept / Reject)
 * @param {string} formId - Form ID to submit
 * @param {string} actionName - "Accept" or "Reject"
 * @param {string} itemType - "Review" or "Testimonial"
 */
window.confirmModerationAction = function (formId, actionName, itemType) {
    const isAccept = actionName.toLowerCase() === 'accept' || actionName.toLowerCase() === 'accepted';
    const title = isAccept ? `Approve ${itemType}?` : `Reject ${itemType}?`;
    const text = isAccept 
        ? `This ${itemType.toLowerCase()} will be published publicly on the storefront.` 
        : `This ${itemType.toLowerCase()} will be hidden from the storefront.`;
    const confirmColor = isAccept ? '#10B981' : '#EF4444';
    const buttonText = isAccept ? 'Yes, approve it' : 'Yes, reject it';

    if (typeof Swal !== 'undefined') {
        Swal.fire({
            title: title,
            text: text,
            icon: isAccept ? 'question' : 'warning',
            showCancelButton: true,
            confirmButtonColor: confirmColor,
            cancelButtonColor: '#64748B',
            confirmButtonText: buttonText,
            cancelButtonText: 'Cancel'
        }).then((result) => {
            if (result.isConfirmed) {
                document.getElementById(formId)?.submit();
            }
        });
    } else {
        if (confirm(`${title}\n${text}`)) {
            document.getElementById(formId)?.submit();
        }
    }
};
