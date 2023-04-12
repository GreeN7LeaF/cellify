document.addEventListener("DOMContentLoaded", function () {
    dropDownSideBar();
    toggleAddProductForm();
    console.log("run into this");
});
// DOMContentLoaded  end
setSideNav();


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

function setSideNav() {
    let route = (window.location.pathname).split("/")[1];
    let route2 = (window.location.pathname).split("/")[2];

    console.log("Route: ", route);

    /*Active SideBar and Header*/
    $item = $('#sidebar .nav-link').filter(function () {
        return $(this).prop('href').split('/').splice(-1)[0].indexOf(route) !== -1 || 
            $(this).prop('href').split('/').splice(-1)[0].indexOf(route2) !== -1;
    });

    $authHeader = $("#auth-header .nav-link").filter(function () {
        return $(this).prop("href").split('/').splice(-1)[0].indexOf(route) !== -1 ||
            $(this).prop('href').split('/').splice(-1)[0].indexOf(route2) !== -1;
    });

    $item.map((idx, val) => val.classList.toggle('active'));
    $authHeader.map((idx, val) => val.classList.toggle("active"));

    /*Pop up sideBar by header*/
    if (route === 'SanPham') {
        $.ajax({
            url: '/PartialHandle/getSideBar',
            data: { headerId: 2 },
            success: function (result) {
                $('#sidebar').html(result);
            }
        });
    } else if (route === 'CuaHang') {
        console.log("Cua hang sidebar rendering...");
        $.ajax({
            url: '/PartialHandle/getSideBar',
            data: { headerId: 3 },
            success: function (result) {
                $('#sidebar').html(result);
            }
        });
    } else if (route === 'TrangChu') {
        $.ajax({
            url: '/PartialHandle/getSideBar',
            data: { headerId: 1 },
            success: function (result) {
                $('#sidebar').html(result);
            }
        });
    }
}

