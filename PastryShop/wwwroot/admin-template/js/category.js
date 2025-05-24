var deleteModal = document.getElementById('deleteModal');
deleteModal.addEventListener('show.bs.modal', function (event) {
    var button = event.relatedTarget;
    var categoryId = button.getAttribute('data-id');
    var input = deleteModal.querySelector('#deleteCategoryId');
    input.value = categoryId;
});


var editModal = document.getElementById('editModal');
    editModal.addEventListener('show.bs.modal', function (event) {
        var button = event.relatedTarget;
        var id = button.getAttribute('data-id');
        var name = button.getAttribute('data-name');
        var isActive = button.getAttribute('data-isactive');

        editModal.querySelector('#editCategoryId').value = id;
        editModal.querySelector('#editCategoryName').value = name;
        editModal.querySelector('#editIsActive').value = isActive.toLowerCase();
    });