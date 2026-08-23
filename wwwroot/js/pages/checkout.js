/**
 * PAGE SCRIPT: CHECKOUT (Index.cshtml)
 */





document.addEventListener('DOMContentLoaded', async () => {
    // getFormOptions();
    const cart = await BarrameruStore.getCart();
    const totals = BarrameruStore.getTotals(cart);
    const container = document.getElementById('checkoutOrderItemsList');

    if (container) {
        if (cart.length === 0) {
            container.innerHTML = '<div class="py-3 text-muted small">No items in order. <a href="/Shop">Go to Shop</a></div>';
        } else {
            container.innerHTML = cart.map(item => `
                <div class="d-flex justify-content-between py-2 border-bottom small text-secondary">
                    <span>${item.productName} × ${item.quantity}</span>
                    <span>$${(item.unitPrice * item.quantity).toFixed(2)}</span>
                </div>
            `).join('');
        }
    }

    const chkSubtotal = document.getElementById('chkSubtotal');
    const chkTotal = document.getElementById('chkTotal');
    if (chkSubtotal) chkSubtotal.textContent = `$${totals.subtotal}`;
    if (chkTotal) chkTotal.textContent = `$${totals.total}`;

    const form = document.getElementById('exactCheckoutForm');
    if (form) {
        form.addEventListener('submit', async (e) => {
            e.preventDefault();

            const formData = new FormData(form);
            try {
                const response = await fetch('/Order/Checkout', {
                    method: 'POST',
                    body: formData
                });
                if (!response.ok) {
                    console.log(response);
                    throw new Error('Failed to place order');
                }
                else {
                    const orderId = await response.text();
                    if (typeof Swal !== 'undefined') {
                        Swal.fire({
                            icon: 'success',
                            title: 'Order Placed Successfully!',
                            text: `Thank you for your order #${orderId}. Redirecting to your invoice...`,
                            confirmButtonColor: '#B67961'
                        }).then(() => {
                            window.location.href = `/Order/Invoice?id=${orderId }`;
                        });
                    } else {
                        window.location.href = `/Order/Invoice?id=${orderId}`;
                    }

                }
            } catch (error) {
                console.error("Error :", error);
            }
        });
    }
});



// async function getFormOptions()
// {
//     const countrySelect = document.getElementById("countrySelect");
//     const citySelect = document.getElementById("citySelect");

//     let countryDataList = [];

//     try {
//         const response = await fetch('/api/Country/GetCountries');
//         console.log(response);
//         if (!response.ok) throw new Error("Failed to load countries");

//         countryDataList = await response.json();

//         countrySelect.innerHTML = '<option selected disabled>Select a country...</option>';
//         countryDataList.forEach(item => {
//             const option = document.createElement("option");
//             option.value = item.country;
//             option.textContent = item.country;
//             countrySelect.appendChild(option);
//         });

//     } catch (error) {
//         console.error("Error fetching countries:", error);
//         countrySelect.innerHTML = '<option selected disabled>Error loading countries</option>';
//     }

//     countrySelect.addEventListener("change", function () {
//         const selectedCountryName = this.value;
//         const matchedCountry = countryDataList.find(item => item.country === selectedCountryName);

//         Clear existing city options
//         citySelect.innerHTML = '<option selected disabled>Select state/city...</option>';

//         if (matchedCountry && matchedCountry.cities.length > 0) {
//             citySelect.removeAttribute("disabled");

//             Populate City Dropdown
//             matchedCountry.cities.forEach(city => {
//                 const option = document.createElement("option");
//                 option.value = city;
//                 option.textContent = city;
//                 citySelect.appendChild(option);
//             });
//         } else {
//             If no cities available or "Other"
//             const option = document.createElement("option");
//             option.value = "N/A";
//             option.textContent = "No cities available";
//             citySelect.appendChild(option);
//             citySelect.setAttribute("disabled", "true");
//         }
//     });
// }

