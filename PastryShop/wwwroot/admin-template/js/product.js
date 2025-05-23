// Edit Modal
var editProductModal = document.getElementById('editProductModal');
editProductModal.addEventListener('show.bs.modal', function (event) {
    var button = event.relatedTarget;
    document.getElementById('editProductId').value = button.getAttribute('data-id');
    document.getElementById('editProductTitle').value = button.getAttribute('data-title');
    document.getElementById('editProductDescription').value = button.getAttribute('data-description');
    document.getElementById('editProductCategory').value = button.getAttribute('data-categoryid');
    document.getElementById('editProductIsActive').value = button.getAttribute('data-isactive').toLowerCase();
});

// Delete Modal
var deleteProductModal = document.getElementById('deleteProductModal');
deleteProductModal.addEventListener('show.bs.modal', function (event) {
    var button = event.relatedTarget;
    document.getElementById('deleteProductId').value = button.getAttribute('data-id');
});