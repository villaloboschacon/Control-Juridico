$('#myModal').on('shown.bs.modal', function () {
    $('#myInput').trigger('focus')
})
function showEditor() {
    $("#EditModal").modal("show");
    $("#EditModal").appendTo("body");
}
function nextMod() {
    $("#exampleModal").show();
    $("#modalAgregar").hide();
}
function priorMod() {
    $("#modalAgregar").show();
    $("#exampleModal").hide();

}
$(document).ready(function () {
    $('tr').click(function () {
        //Check to see if background color is set or if it's set to white.
        if (this.style.background == "" || this.style.background == "white") {
            $(this).css('background', '#93b8c4');
        }
        else {
            $(this).css('background', 'white');
        }
    });
});
var dropdown = document.getElementsByClassName("dropdown-btn");
var i;
for (i = 0; i < dropdown.length; i++) {
    dropdown[i].addEventListener("click", function () {
        this.classList.toggle("active");
        var dropdownContent = this.nextElementSibling;
        if (dropdownContent.style.display === "block") {
            dropdownContent.style.display = "none";
        } else {
            dropdownContent.style.display = "block";
        }
    });
}

