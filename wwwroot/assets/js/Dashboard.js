const shrink_btn = document.querySelector(".shrink-btn");
const search = document.querySelector(".search");
const sidebar_links = document.querySelectorAll(".sidebar-links a");
const active_tab = document.querySelector(".active-tab");
const shortcuts = document.querySelector(".sidebar-links h4");
const tooltip_elements = document.querySelectorAll(".tooltip-element");

let activeIndex;

shrink_btn.addEventListener("click", () => {
  document.body.classList.toggle("shrink");
  setTimeout(moveActiveTab, 400);

  shrink_btn.classList.add("hovered");

  setTimeout(() => {
    shrink_btn.classList.remove("hovered");
  }, 500);
});

search.addEventListener("click", () => {
  document.body.classList.remove("shrink");
  search.lastElementChild.focus();
});

// function moveActiveTab() {
//   let topPosition = activeIndex * 58 + 2.5;

//   if (activeIndex > 3) {
//     topPosition += shortcuts.clientHeight;
//   }

//   active_tab.style.top = `${topPosition}px`;
// }

// function changeLink(event) {
//   sidebar_links.forEach((sideLink) => sideLink.classList.remove("active"));
//   this.classList.add("active");

//   activeIndex = this.dataset.active;

//   moveActiveTab();
// }

// sidebar_links.forEach((link) => link.addEventListener("click", changeLink));

// function showTooltip() {
//   let tooltip = this.parentNode.lastElementChild;
//   let spans = tooltip.children;
//   let tooltipIndex = this.dataset.tooltip;

//   Array.from(spans).forEach((sp) => sp.classList.remove("show"));
//   spans[tooltipIndex].classList.add("show");

//   tooltip.style.top = `${(100 / (spans.length * 2)) * (tooltipIndex * 2 + 1)}%`;
// }

// tooltip_elements.forEach((elem) => {
//   elem.addEventListener("mouseover", showTooltip);
// });

/**
 * Add other Image
 */
function addImageInput() {
  // Create a new input element
  var newInput = document.createElement("input");
  newInput.type = "file";
  newInput.name = "image";
  newInput.className = "form-control";
  newInput.accept = "image/*";
  newInput.required = true;

  // Append the new input to the container
  document.getElementById("imageInputsContainer").appendChild(newInput);
}
function addDescription() {
    // Create a new input element
    var newInputName = document.createElement("input");
    newInputName.type = "text";
    newInputName.name = "DescriptionName[]";
    newInputName.className = "form-control";
    newInputName.required = true;
    document.getElementById("itemNameContainer").appendChild(newInputName);

    var newInputDescription = document.createElement("input");
    newInputDescription.type = "textarea";
    newInputDescription.name = "DescriptionDes[]";
    newInputDescription.className = "form-control";
    newInputDescription.required = true;
    newInputDescription.setAttribute("rows",3);
    document.getElementById("itemNameContainer").appendChild(newInputDescription);


    // Append the new input to the container
}
