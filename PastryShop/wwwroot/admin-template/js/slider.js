// Edit Modal
var editSliderModal = document.getElementById('editModal');
editSliderModal.addEventListener('show.bs.modal', function (event) {
    var button = event.relatedTarget;
    document.getElementById('editSliderId').value = button.getAttribute('data-id');
    document.getElementById('editTitle').value = button.getAttribute('data-title');
    document.getElementById('editDescription').value = button.getAttribute('data-description');
    document.getElementById('editOrder').value = button.getAttribute('data-order');
    document.getElementById('editBorderColor').value = button.getAttribute('data-bordercolor');
    document.getElementById('editBackgroundColor').value = button.getAttribute('data-backgroundcolor');
    document.getElementById('editIsActive').value = button.getAttribute('data-isactive').toLowerCase();
});

// Delete Modal
var deleteSliderModal = document.getElementById('deleteModal');
deleteSliderModal.addEventListener('show.bs.modal', function (event) {
    var button = event.relatedTarget;
    document.getElementById('deleteSliderId').value = button.getAttribute('data-id');
});
