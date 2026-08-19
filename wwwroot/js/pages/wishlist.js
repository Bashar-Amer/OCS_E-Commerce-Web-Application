/**
 * PAGE SCRIPT: WISHLIST (Index.cshtml)
 */

window.renderWishlistPage = function() {
    const wishlist = BarrameruStore.getWishlist();
    const tbody = document.getElementById('wishlistTableBody');
    if (!tbody) return;

    if (wishlist.length === 0) {
        tbody.innerHTML = '<tr><td colspan="6" class="text-center py-5 text-muted">Your wishlist is empty. <a href="/Shop" class="text-primary fw-bold">Explore Gear</a></td></tr>';
        return;
    }

    tbody.innerHTML = wishlist.map(item => `
        <tr class="border-bottom">
            <td>
                <button class="btn btn-sm text-danger border-0" onclick="BarrameruStore.toggleWishlist(${item.id}); window.renderWishlistPage();">
                    <i class="bi bi-x-circle fs-5"></i>
                </button>
            </td>
            <td>
                <img src="${item.image}" alt="${item.name}" style="width: 50px; height: 50px; object-fit: contain;">
            </td>
            <td>
                <h6 class="mb-0 small fw-bold"><a href="/Shop/Details/${item.id}">${item.name}</a></h6>
            </td>
            <td class="small fw-bold text-secondary">$${item.price.toFixed(2)}</td>
            <td>
                <span class="wishlist-stock-in"><i class="bi bi-check-circle-fill me-1"></i> In Stock</span>
            </td>
            <td>
                <button class="btn btn-barrameru btn-sm" onclick="BarrameruStore.addToCart(${item.id}, 1); BarrameruStore.toggleWishlist(${item.id}); window.renderWishlistPage();">
                    Move to Cart
                </button>
            </td>
        </tr>
    `).join('');
};

document.addEventListener('DOMContentLoaded', () => {
    // Initialize standard wishlist item if empty
    if (!localStorage.getItem('barrameru_wishlist')) {
        BarrameruStore.saveWishlist([
            { id: 2, name: "Yellow Tent", category: "Tent & Accesories", price: 65.00, image: "/images/orange-tourist-tent-illuminated-from-inside-stands-in-mountains-above-clouds.jpg", stock: 8 },
            { id: 9, name: "Trekking Backpack", category: "Bags & Pack", price: 150.00, image: "/images/blue-hiking-backpack-with-fitness-mat-isolated-on-2021-09-03-13-40-43-utc-1.jpg", stock: 14 }
        ]);
    }
    window.renderWishlistPage();
});
