
$(document).ready(function () {

    loadNotebooks();

    // Load all notebooks using $.get()
    function loadNotebooks() {

        $.get('/Notebook/GetAll', function (data) {

            $('#notebookList').empty();

            if (data.length === 0) {
                $('#notebookList').append(
                    '<tr><td colspan="3">No notebooks found.</td></tr>'
                );
                return;
            }

            $.each(data, function (index, notebook) {

                let row = $('<tr>');

                $('<td>').text(notebook.title).appendTo(row);
                $('<td>').text(notebook.description || '').appendTo(row);

                let actions = $('<td>');

                $('<button>')
                    .text('Edit')
                    .addClass('btn btn-warning btn-sm me-2')
                    .on('click', function () {
                        editNotebook(notebook);
                    })
                    .appendTo(actions);

                $('<button>')
                    .text('Delete')
                    .addClass('btn btn-danger btn-sm')
                    .on('click', function () {
                        deleteNotebook(notebook.id);
                    })
                    .appendTo(actions);

                row.append(actions);
                $('#notebookList').append(row);
            });

        }).fail(function () {
            showMessage('Unable to load notebooks.', 'danger');
        });
    }

    // Create or update notebook
    $('#notebookForm').submit(function (e) {

        e.preventDefault();

        let id = $('#notebookId').val();
        let url = id == 0
            ? '/Notebook/Create'
            : '/Notebook/Update';

        let formData = $(this).serialize();

        // Create using $.post()
        if (id == 0) {

            $.post(url, formData, function (response) {

                showMessage('Notebook created successfully!', 'success');

                resetForm();
                loadNotebooks();

            }).fail(function (xhr) {
                showMessage(
                    xhr.responseJSON?.message || 'Unable to create notebook.',
                    'danger'
                );
            });

        }
        // Update using $.ajax()
        else {

            $.ajax({
                url: url,
                type: 'POST',
                data: formData,
                success: function () {

                    showMessage('Notebook updated successfully!', 'success');

                    resetForm();
                    loadNotebooks();
                },
                error: function (xhr) {
                    showMessage(
                        xhr.responseJSON?.message || 'Unable to update notebook.',
                        'danger'
                    );
                }
            });
        }
    });

    // Fill form for editing
    function editNotebook(notebook) {

        $('#notebookId').val(notebook.id);
        $('#title').val(notebook.title);
        $('#description').val(notebook.description);

        $('#saveButton').text('Update Notebook');
        $('#cancelButton').show();

        window.scrollTo(0, 0);
    }

    // Delete notebook using $.ajax()
    function deleteNotebook(id) {

        if (!confirm('Are you sure you want to delete this notebook?')) {
            return;
        }

        let token = $('input[name="__RequestVerificationToken"]').val();

        $.ajax({
            url: '/Notebook/Delete',
            type: 'POST',
            data: {
                id: id,
                __RequestVerificationToken: token
            },
            success: function () {

                showMessage('Notebook deleted successfully!', 'success');
                loadNotebooks();
            },
            error: function (xhr) {
                showMessage(
                    xhr.responseJSON?.message || 'Unable to delete notebook.',
                    'danger'
                );
            }
        });
    }

    // Cancel editing
    $('#cancelButton').click(function () {
        resetForm();
    });

    // Reset form
    function resetForm() {

        $('#notebookForm')[0].reset();

        $('#notebookId').val(0);
        $('#saveButton').text('Create Notebook');
        $('#cancelButton').hide();
    }

    // Display messages
    function showMessage(message, type) {

        $('#message')
            .removeClass('text-success text-danger')
            .addClass(type === 'success' ? 'text-success' : 'text-danger')
            .text(message);
    }

});
