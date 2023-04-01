document.addEventListener("DOMContentLoaded", function () {
    dropDownSideBar();
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

function setSideNav() {
    let route = (window.location.pathname).split("/")[1];
    /*console.log($("#sidebar").find(`[href="${route}"]`));*/

    $item = $('#sidebar .nav-link').filter(function () {
        return $(this).prop('href').indexOf(route) !== -1;
    });

    $authHeader = $("#auth-header .nav-link").filter(function () {
        return $(this).prop("href").indexOf(route) !== -1;
    });

    $item.map((idx, val) => console.log(val.classList.toggle('active')));
    $authHeader.map((idx, val) => console.log(val.classList.toggle("active")));
}
