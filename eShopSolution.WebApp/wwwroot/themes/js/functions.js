var SiteController = function () {
    this.initializeHeader = function () {
         regsiterEvents();
        LoadCart();
    }
    function LoadCart() {
        const currentID = $('#hidCulture').val();
        $.ajax({
            type: "Get",
            url: "/" + currentID + '/cart/GetListItems/',
            success: function (res) {
                $('#lbl_number_of_items_header').text(res.length);
            }
        })
    }
    function regsiterEvents() {
        $('body').on('click', '.btn-add-cart', function (e) {
            e.preventDefault();
            const id = $(this).data('id');
            const currentID = $('#hidCulture').val();
            $.ajax({
                type: "POST",
                url: "/" + currentID + '/cart/AddToCart/',
                data: {
                    id: id,
                    languageId: currentID
                },
                success: function (res) {
                    $('#lbl_number_of_items_header').text(res.length);
                },
                error: function (err) {
                    console.log(err);
                }
            })
        });
    }
}

var CartController = function () {
    this.initialize = function () {
        loadData();
    }
    function loadData() {
        const currentID = $('#hidCulture').val();
        const urlId = $('#hidUrl').val();
        $.ajax({
            type: "Get",
            url: "/" + currentID + '/cart/GetListItems/',
            success: function (res) {
                var html = '';
                var total = 0;
                $.each(res, function (i, item) {
                    var amount = item.price * item.quantity
                    html += `<tr>
                        <td> <img width="60" src="${urlId + "/" + item.image}" alt="" /></td>
                        <td>${item.description}</td>
                        <td>
                            <div class="input-append">
                                <input class="span1" style="max-width:34px" placeholder="1" id="appendedInputButtons" size="16" type="text"><button class="btn" type="button">
                                <i class="icon-minus"></i>
                                </button><button class="btn" type="button">
                                <i class="icon-plus"></i></button><button class="btn btn-danger" type="button">
                                <i class="icon-remove icon-white"></i></button></div>
                        </td>
                        <td>${numberWithCommas(item.price)}</td>
                        <td>${numberWithCommas(amount)}</td>
                    </tr>`
                    total += amount;
                });
                $('#cart_body').html(html);
                $('#lbl_number_of_items').text(res.length);
                $('#lbl_total').text(numberWithCommas(total));
            },
            error: function (err) {
                console.log(err);
            }
        })
    }
}
function numberWithCommas(x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}