function renderSideNav(route, route2) {
    let headerId;
    switch (route) {
        case 'SanPham':
            headerId = 2;
            break;
        case 'CuaHang':
            headerId = 3;
            break;
        case 'TrangChu':
            headerId = 1;
            break;
        default:
            return;
    }
    $.ajax({
        url: '/PartialHandle/getSideBar',
        data: { headerId },
        success: function (result) {
            $('#sidebar').html(result);
            setSideNav(route, route2);
            dropDownSideBar();
            toggleAddProductForm();
        }
    });
}

// DOMContentLoaded  end
$(document).ready(function () {
    let route = (window.location.pathname).split("/")[1];
    let route2 = (window.location.pathname).split("/")[2];
    if ($("#admin").length > 0) {
        if (route == "Index" || route == "index") route = "noooo";
        if (route2 == "Index" || route2 == "index" ) route2 = "noooo";
        console.log(route, "   ", route2);

        renderSideNav(route, route2);
    }
    tabHandler();
    setHeader(route, route2);
    if ($(".pages")) {
        setPageList();
    }   

    /*var input = document.getElementById("HinhAnh");
    console.log(input);
    input.value = input.getAttribute("data-default-value");
    console.log(input.value);*/
});

function dropDownSideBar() {
  document.querySelectorAll("#sidebar .nav-link").forEach(function (element) {
    element.addEventListener("click", function (e) {
      let nextEl = element.nextElementSibling;
      let parentEl = element.parentElement;

      if (nextEl) {
        e.preventDefault();
        let mycollapse = new bootstrap.Collapse(nextEl);

        if (nextEl.classList.contains("show")) {
          mycollapse.hide();
        } else {
          mycollapse.show();
          this.toggle("active");
          // find other submenus with class=show
          var opened_submenu =
            parentEl.parentElement.querySelector(".submenu.show");
          // if it exists, then close all of them
          if (opened_submenu) {
            new bootstrap.Collapse(opened_submenu);
          }
        }
      }
    }); // addEventListener
  }); // forEach
}

function toggleAddProductForm() {
    $('#addProduct').click(() => {
        $('#addProductForm').removeClass('d-none');
        $('#addProduct').addClass('d-none');
    });

    $('#removeAddProductForm').click(() => {
        $('#addProductForm').addClass('d-none');
        $('#addProduct').removeClass('d-none');    
    });
}

function setSideNav(route, route2) {
    /*Active SideBar and Header*/
    $item = $('#sidebar .nav-link').filter(function () {

        return $(this).prop('href').split('/').splice(-1)[0].indexOf(route) !== -1 || 
            $(this).prop('href').split('/').splice(-1)[0].indexOf(route2) !== -1;
    });
    

    $item.map((idx, val) => val.classList.toggle('active'));
}

function setHeader(route, route2) {
    $authHeader = $("#auth-header .nav-link").filter(function () {
        return $(this).prop("href").split('/').splice(-1)[0]==(route) ||
            $(this).prop('href').split('/').splice(-1)[0]==(route2) ||
            $(this).prop("href").split('/').splice(-2)[0]==(route) ||
            $(this).prop('href').split('/').splice(-2)[0]==(route2);
    });

    $authHeader.map((idx, val) => val.classList.toggle("active"));
}

function setPageList() {
    $(".pages .page-link").map((idx, val) => {
        $(this).removeClass(".active");
    };
    
    let param = (window.location.toString()).split("=")[1];

    if (param) {
        $pageLink = $(".pages .page-link").filter(function () {
            //lay value cua page
            var getValueFromString = $(this).prop("href").split("=")[1];
            if (getValueFromString.length > 1) getValueFromString = getValueFromString.substring(0, getValueFromString.length - getValueFromString.length + 1);
            return getValueFromString == param;
        });
        $pageLink.map((idx, val) => {
            val.classList.toggle("active"));
        };
    } else {
        $(".pages .page-link").eq(0).classList.toggle("active"));
    }
}

function tabHandler() {
    $(".best-product-tab-nav li.nav-item").on("click", function () {
        //get product type id
        var typeid = $(".best-product-tab-nav li.nav-item .nav-link.active").parent().attr('id');
        var id = parseInt(typeid);
        if (id > 0 && typeof id === 'number') {
            $.ajax({
                url: '/PartialHandle/getProductTab',
                data: { id },
                success: function (result) {
                    $('#productTabContainer').html(result);
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    console.log(xhr.status);
                    console.log(thrownError);
                }
            });
        }
    });
    /*$(".tab-slider.nav-center .navlink.active").parent().attr('id');*/
    $(".product-list li.nav-item").on("click", function () {
        //get product type id
        var typeid = $(".product-list li.nav-item .nav-link.active").parent().attr('id');
        var id = parseInt(typeid.split('-')[1]);
        if (id > 0 && typeof id === 'number') {
            $.ajax({
                url: '/PartialHandle/getProductTab',
                data: { id },
                success: function (result) {
                    $('#productListContainer').html(result);
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    console.log(xhr.status);
                    console.log(thrownError);
                }
            });
        }
    });
}

