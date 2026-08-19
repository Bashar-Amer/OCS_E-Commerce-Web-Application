/**
 * Barrameru Outdoor & Camping Store - Main UI Controller & Interactions
 */

document.addEventListener('DOMContentLoaded', () => {
  // 1. Sticky Navigation Bar
  const header = document.querySelector('.header-main');
  if (header) {
    window.addEventListener('scroll', () => {
      if (window.scrollY > 50) {
        header.classList.add('is-sticky');
      } else {
        header.classList.remove('is-sticky');
      }
    });
  }

  // 2. Quantity Selector (+ / -) Controller
  document.addEventListener('click', (e) => {
    if (e.target.closest('.qty-plus-btn')) {
      const input = e.target.closest('.quantity-picker').querySelector('.quantity-input');
      if (input) {
        input.value = parseInt(input.value || 1) + 1;
        input.dispatchEvent(new Event('change'));
      }
    }
    if (e.target.closest('.qty-minus-btn')) {
      const input = e.target.closest('.quantity-picker').querySelector('.quantity-input');
      if (input && parseInt(input.value) > 1) {
        input.value = parseInt(input.value) - 1;
        input.dispatchEvent(new Event('change'));
      }
    }
  });

  // 3. Product Details Image Thumbnail Switcher
  const mainGalleryImg = document.getElementById('mainGalleryImage');
  const thumbs = document.querySelectorAll('.product-thumb-item');
  if (mainGalleryImg && thumbs.length > 0) {
    thumbs.forEach(thumb => {
      thumb.addEventListener('click', function() {
        thumbs.forEach(t => t.classList.remove('active'));
        this.classList.add('active');
        const newSrc = this.getAttribute('data-img');
        if (newSrc) {
          mainGalleryImg.style.opacity = '0.3';
          setTimeout(() => {
            mainGalleryImg.src = newSrc;
            mainGalleryImg.style.opacity = '1';
          }, 150);
        }
      });
    });
  }

  // 4. Rating Star Picker on Review Submission
  const starPickers = document.querySelectorAll('.star-picker-icon');
  const ratingInput = document.getElementById('selectedRatingInput');
  if (starPickers.length > 0 && ratingInput) {
    starPickers.forEach(star => {
      star.addEventListener('click', function() {
        const val = parseInt(this.getAttribute('data-value'));
        ratingInput.value = val;
        starPickers.forEach((s, idx) => {
          if (idx < val) {
            s.classList.remove('bi-star');
            s.classList.add('bi-star-fill');
            s.style.color = '#f59e0b';
          } else {
            s.classList.remove('bi-star-fill');
            s.classList.add('bi-star');
            s.style.color = '#d1d5db';
          }
        });
      });
    });
  }

  // 5. Global Newsletter Form Submission
  const newsletterForms = document.querySelectorAll('.newsletter-form');
  newsletterForms.forEach(form => {
    form.addEventListener('submit', (e) => {
      e.preventDefault();
      const emailInput = form.querySelector('input[type="email"]');
      if (emailInput && emailInput.value.trim() !== '') {
        if (typeof Swal !== 'undefined') {
          Swal.fire({
            icon: 'success',
            title: 'Thank you for subscribing!',
            text: 'You will receive our latest outdoor deals and camping tips directly in your inbox.',
            confirmButtonColor: '#B67961'
          });
        }
        form.reset();
      }
    });
  });

  // 6. Global Contact Form Submission
  const contactForm = document.getElementById('barrameruContactForm');
  if (contactForm) {
    contactForm.addEventListener('submit', (e) => {
      e.preventDefault();
      if (typeof Swal !== 'undefined') {
        Swal.fire({
          icon: 'success',
          title: 'Message Sent Successfully!',
          text: 'Thank you for reaching out. Our adventure gear team will get back to you within 24 hours.',
          confirmButtonColor: '#B67961'
        });
      }
      contactForm.reset();
    });
  }

  // 7. Product Review Submission Form
  const reviewForm = document.getElementById('productReviewForm');
  if (reviewForm) {
    reviewForm.addEventListener('submit', (e) => {
      e.preventDefault();
      if (typeof Swal !== 'undefined') {
        Swal.fire({
          icon: 'success',
          title: 'Review Submitted!',
          text: 'Thank you for your rating! Your review has been submitted and is pending admin approval.',
          confirmButtonColor: '#B67961'
        });
      }
      reviewForm.reset();
    });
  }

  // 8. Testimonial Submission Form (Home / About)
  const testimonialForm = document.getElementById('testimonialForm');
  if (testimonialForm) {
    testimonialForm.addEventListener('submit', (e) => {
      e.preventDefault();
      if (typeof Swal !== 'undefined') {
        Swal.fire({
          icon: 'success',
          title: 'Testimonial Received!',
          text: 'Thank you for sharing your experience. It will appear after admin review.',
          confirmButtonColor: '#B67961'
        });
      }
      testimonialForm.reset();
    });
  }
});
