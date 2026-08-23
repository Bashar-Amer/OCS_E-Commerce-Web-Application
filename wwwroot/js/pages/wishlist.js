/**
 * PAGE SCRIPT: WISHLIST
 */

async function removeWishlistItem(wishlistItemId, button) {

    const token = document.querySelector(
        '#wishlistAntiForgeryForm input[name="__RequestVerificationToken"]'
    )?.value;

    if (!token) {
        console.error("Anti-forgery token not found.");
        return;
    }


    button.disabled = true;


    try {

        const response = await fetch('/Wishlist/Remove', {

            method: 'POST',

            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            },

            body: new URLSearchParams({
                wishlistItemId: wishlistItemId,
                __RequestVerificationToken: token
            })

        });


        if (!response.ok) {

            if (response.status === 401) {
                window.location.href = '/Account/Login';
                return;
            }

            throw new Error('Failed to remove wishlist item.');
        }


        const result = await response.json();


        if (result.success) {

            // Remove row from UI
            const row = button.closest('tr');

            if (row) {
                row.remove();
            }


            // Check if wishlist is now empty
            const tbody = document.getElementById('wishlistTableBody');

            if (tbody && tbody.querySelectorAll('tr').length === 0) {

                tbody.innerHTML = `
                    <tr>
                        <td colspan="6" class="text-center py-5">

                            <div class="py-4">

                                <i class="bi bi-heart fs-1 text-muted"></i>

                                <h5 class="fw-bold mt-3">
                                    Your wishlist is empty
                                </h5>

                                <p class="text-muted small mb-4">
                                    Save your favorite camping gear here.
                                </p>

                                <a href="/Shop"
                                   class="btn btn-barrameru">

                                    Explore Gear

                                </a>

                            </div>

                        </td>
                    </tr>
                `;
            }

        }

    }
    catch (error) {

        console.error(error);

        button.disabled = false;

        alert("Something went wrong. Please try again.");

    }
}


/* =========================================================
   GUEST WISHLIST
   ========================================================= */

window.renderWishlistPage = function () {

    const wishlist = BarrameruStore.getWishlist();

    const tbody = document.getElementById('wishlistTableBody');

    if (!tbody) return;


    if (wishlist.length === 0) {

        tbody.innerHTML = `
            <tr>

                <td colspan="6"
                    class="text-center py-5">

                    <div class="py-4">

                        <i class="bi bi-heart fs-1 text-muted"></i>

                        <h5 class="fw-bold mt-3">
                            Your wishlist is empty
                        </h5>

                        <p class="text-muted small mb-4">
                            Save your favorite camping gear here.
                        </p>

                        <a href="/Shop"
                           class="btn btn-barrameru">

                            Explore Gear

                        </a>

                    </div>

                </td>

            </tr>
        `;

        return;
    }


    tbody.innerHTML = wishlist.map(item => `

        <tr class="border-bottom">

            <!-- Remove -->

            <td>

                <button type="button"
                        class="btn btn-sm text-danger border-0"
                        onclick="
                            BarrameruStore.toggleWishlist(${item.id});
                            window.renderWishlistPage();
                        "
                        title="Remove from wishlist">

                    <i class="bi bi-x-circle fs-5"></i>

                </button>

            </td>


            <!-- Image -->

            <td>

                <a href="/Shop/Details/${item.id}">

                    <img src="${item.image || '/images/placeholder.jpg'}"
                         alt="${item.name}"
                         class="img-fluid"
                         style="
                            width: 65px;
                            height: 65px;
                            object-fit: contain;
                         ">

                </a>

            </td>


            <!-- Product -->

            <td>

                <h6 class="mb-0 fw-bold">

                    <a href="/Shop/Details/${item.id}"
                       class="text-dark text-decoration-none">

                        ${item.name}

                    </a>

                </h6>

            </td>


            <!-- Price -->

            <td>

                <span class="fw-bold text-secondary">

                    $${Number(item.price).toFixed(2)}

                </span>

            </td>


            <!-- Stock -->

            <td>

                <span class="text-success small">

                    <i class="bi bi-check-circle-fill me-1"></i>

                    In Stock

                </span>

            </td>


            <!-- Action -->

            <td>

                <button type="button"
                        class="btn btn-barrameru btn-sm"
                        onclick="
                            BarrameruStore.addToCart(${item.id}, 1);
                            BarrameruStore.toggleWishlist(${item.id});
                            window.renderWishlistPage();
                        ">

                    <i class="bi bi-cart3 me-1"></i>

                    Add to Cart

                </button>

            </td>

        </tr>

    `).join('');
};


/* =========================================================
   PAGE INITIALIZATION
   ========================================================= */
document.addEventListener('DOMContentLoaded', function () {

    const tbody = document.getElementById('wishlistTableBody');

    if (!tbody) return;

    const isLoggedIn = tbody.dataset.loggedIn === "true";

    if (!isLoggedIn) {
        window.renderWishlistPage();
    }

});