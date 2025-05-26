// Edit Modal
var editAboutModal = document.getElementById('editModal');
editAboutModal.addEventListener('show.bs.modal', function (event) {
    var button = event.relatedTarget;
    document.getElementById('editId').value = button.getAttribute('data-id');
    document.getElementById('editTitle').value = button.getAttribute('data-title');
    document.getElementById('editDescription').value = button.getAttribute('data-description');
    document.getElementById('imageUrl').value = button.getAttribute('data-image');
});

// Delete Modal
var deleteSliderModal = document.getElementById('deleteModal');
deleteSliderModal.addEventListener('show.bs.modal', function (event) {
    var button = event.relatedTarget;
    document.getElementById('deleteAboutId').value = button.getAttribute('data-id');
});
