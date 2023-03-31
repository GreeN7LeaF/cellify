document.addEventListener("DOMContentLoaded", function () {
    dropDownSideBar();
    setSideNav();
});
// DOMContentLoaded  end

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
    document.addEventListener("DOMContentLoaded", function () {
        document
            .querySelectorAll("#sidebar .navlink")
            .forEach(function (element) {
                element.addEventListener("click", function (e) {
                    console.log("click");
                    let nextEl = element.nextElementSibling;
                    let parentEl = element.parentElement;

                    if (nextEl) {
                        e.preventDefault();
                        let mycollapse = new bootstrap.Collapse(nextEl);

                        if (nextEl.classList.contains("show")) {
                            mycollapse.hide();
                        } else {
                            mycollapse.show();
                            // find other submenus with class=show
                            var opened_submenu =
                                parentEl.parentElement.querySelector(
                                    ".submenu.show"
                                );
                            // if it exists, then close all of them
                            if (opened_submenu) {
                                new bootstrap.Collapse(opened_submenu);
                            }
                        }
                    }
                }); // addEventListener
            }); // forEach
    });
    // DOMContentLoaded  end
}
