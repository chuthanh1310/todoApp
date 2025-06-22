// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function editTodo(span) {
    const container = span.closest("div[data-id]");
    const editForm = container.querySelector(".edit-form");
    const input = editForm.querySelector("input[name='Title']");
    span.style.display = "none";
    editForm.style.display = "inline-block";
    input.focus();
}

function submitEdit(input) {
    const form = input.closest("form");
    if (input.value.trim() !== "") {
        form.submit();
    } else {
        alert("Không được để trống tiêu đề!");
    }
}

function checkEnter(event, input) {
    if (event.key === "Enter") {
        event.preventDefault();
        submitEdit(input);
    }
}