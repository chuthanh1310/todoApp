function enableEdit(span) {
    const todoItem = span.closest(".todo-item"); // thay vì .todo-left
    const checkbox = todoItem.querySelector(".todo-checkbox");
    const deleteBtn = todoItem.querySelector(".delete-form");
    const editForm = todoItem.querySelector(".edit-form");

    if (checkbox) checkbox.style.display = "none";
    if (deleteBtn) deleteBtn.style.display = "none";

    span.style.display = "none";
    editForm.style.display = "inline";

    const input = editForm.querySelector("input[type='text']");
    input.focus();
    input.select();
}

function submitOnEnter(event, input) {
    if (event.key === 'Enter') {
        event.preventDefault();
        input.form.submit();
    } else if (event.key === 'Escape') {
        const todoItem = input.closest(".todo-item");
        const checkbox = todoItem.querySelector(".todo-checkbox");
        const span = todoItem.querySelector(".todo-title");
        const deleteBtn = todoItem.querySelector(".delete-form");

        if (checkbox) checkbox.style.display = "inline";
        if (deleteBtn) deleteBtn.style.display = "inline";

        span.style.display = "inline";
        input.form.style.display = "none";
    }
}
