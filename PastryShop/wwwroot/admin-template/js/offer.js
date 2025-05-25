// Edit Modal
var editOfferModal = document.getElementById('editModal');
editOfferModal.addEventListener('show.bs.modal', function (event) {
    var button = event.relatedTarget;
    document.getElementById('editId').value = button.getAttribute('data-id');
    document.getElementById('editStartDate').value = button.getAttribute('data-start-date');
    document.getElementById('editEndDate').value = button.getAttribute('data-end-date');
    document.getElementById('editIsActive').value = button.getAttribute('data-isactive').toLowerCase();
});

// Delete Modal
var deleteSliderModal = document.getElementById('deleteModal');
deleteSliderModal.addEventListener('show.bs.modal', function (event) {
    var button = event.relatedTarget;
    document.getElementById('deleteOfferId').value = button.getAttribute('data-id');
});
