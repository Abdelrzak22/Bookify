//const KTThemeMode = require("../assets/js/scripts.bundle");

//var UpdatedRow;
//var table;
//var datatable;
//var exportcols = [];


//function SuccessAlert(message = "Saved Successfully") {
//    Swal.fire({
//        title: "success",
//        icon: "success",
//        Text:message,
//        draggable: true
//    });
//}

//function ErrorAlert(message = "Something wont Wrong!") {
//    Swal.fire({
//        title: "error",
//        icon: "error",
//        Text: message,
//        draggable: true
//    });


//}

//function onModalbegin() {
//    $('body :submit').attr('disabled', 'disabled').attr('data-kt-indicator', 'on');
//}
//function onModalSuccess(row) {
//    SuccessAlert();
//    $('#exampleModal').modal('hide');

//    if (UpdatedRow !== undefined) {

//        datatable.row(UpdatedRow).remove().draw();
//        UpdatedRow = undefined;
//    }
//    var newrow = $(row);
//    datatable.row.add(newrow).draw();


//    KTMenu.init();
//    KTMenu.initHandlers();

//}

//function onModalcomplete() {
//    $('body :submit').removeAttr('disabled').removeAttr('data-kt-indicator');
//}



//// datatables ,search and export

//var headers = $('th');
//$.each(headers, function (i) {
//    if (!$(this).hasClass('js-no-export'))
//        exportcols.push(i);

//})

//var KTDatatableExample = function () {
//    // Shared variables


//    // Private functions
//    var initDatatable = function () {
//        if (!table) return;

//        try {
//            // Set date data order for proper sorting (if you have date columns)
//            const tableRows = table.querySelectorAll('tbody tr');
//            tableRows.forEach(row => {
//                const dateRow = row.querySelectorAll('td');
//                if (dateRow[2]) { // Created On column
//                    const realDate = moment(dateRow[2].innerHTML).format();
//                    dateRow[2].setAttribute('data-order', realDate);
//                }
//                if (dateRow[3]) { // Last Updated On column
//                    const realDate = moment(dateRow[3].innerHTML).format();
//                    dateRow[3].setAttribute('data-order', realDate);
//                }
//            });

//            // Initialize Datatable
//            datatable = $(table).DataTable({
//                "info": true,
//                "order": [[0, 'asc']], // Order by Name column
//                "pageLength": 10,
//                "paging": true,
//                "searching": true,
//                "lengthChange": true,
//                "pageLength": 10,
//                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
//                "dom": 't<"row"<"col-sm-12 col-md-6"i><"col-sm-12 col-md-6"p>>', // Show table, info, and pagination
//                "buttons": [
//                    {
//                        extend: 'copyHtml5',
//                        title: 'Categories Report',
//                        exportOptions: {
//                            columns:exportcols
//                        }
//                    },
//                    {
//                        extend: 'excelHtml5',
//                        title: 'Categories Report',
//                        exportOptions: {
//                            columns: exportcols
//                        }
//                    },
//                    {
//                        extend: 'csvHtml5',
//                        title: 'Categories Report',
//                        exportOptions: {
//                            columns: exportcols
//                        }
//                    },
//                    {
//                        extend: 'pdfHtml5',
//                        title: 'Categories Report',
//                        orientation: 'portrait',
//                        pageSize: 'A4',
//                        exportOptions: {
//                            columns: exportcols
//                        }
//                    }
//                ]
//            });

//            // Create hidden container for buttons if it doesn't exist
//            if ($('#kt_datable_example_buttons').length === 0) {
//                $('<div id="kt_datable_example_buttons" style="display:none;"></div>').appendTo('body');
//            }

//            // Append export buttons to hidden container
//            datatable.buttons().container().appendTo('#kt_datable_example_buttons');

//            console.log('DataTable initialized successfully');
//        } catch (error) {
//            console.error('Error initializing DataTable:', error);
//        }
//    };

//    // Hook export buttons to dropdown menu items
//    var handleExportButtons = function () {
//        const exportButtons = document.querySelectorAll('#kt_datable_example_export_menu [data-kt-export]');
//        if (exportButtons.length === 0) {
//            console.log('No export buttons found');
//            return;
//        }

//        exportButtons.forEach(exportButton => {
//            exportButton.addEventListener('click', function (e) {
//                e.preventDefault();
//                const exportValue = this.getAttribute('data-kt-export');
//                let buttonClass = exportValue;
//                if (exportValue === 'excel') {
//                    buttonClass = 'excel';
//                }
//                const target = document.querySelector('.dt-buttons .buttons-' + buttonClass);
//                if (target) {
//                    target.click();
//                    console.log('Export triggered:', exportValue);
//                } else {
//                    console.log('Export button not found:', buttonClass);
//                }
//            });
//        });
//    };

//    // Handle search functionality
//    var handleSearchDatatable = function () {
//        const filterSearch = document.querySelector('[data-kt-filter="search"]');
//        if (filterSearch) {
//            filterSearch.addEventListener('keyup', function (e) {
//                if (datatable) {
//                    datatable.search(e.target.value).draw();
//                    console.log('Searching for:', e.target.value);
//                }
//            });
//        }
//    };

//    // Public methods
//    return {
//        init: function () {
//            table = document.querySelector('.js-datatables');

//            if (!table) {
//                console.log('Table not found - check if table exists');
//                return;
//            }

//            console.log('Table found, initializing...');
//            initDatatable();
//            handleExportButtons();
//            handleSearchDatatable();
//        }
//    };
//}();


//$(document).ready(function () {

//    ////TinyMCE
//    //var options = { selector: ".js-tinymce", height: "422" };

//    //if (KTThemeMode.getMode() === "dark") {
//    //    options["skin"] = "oxide-dark";
//    //    options["content_css"] = "dark";
//    //}

//    //tinymce.init(options);

//    //$('form').on('submit', function () {
//    //    tinymce.triggerSave();
//    //});


//    //select list
//    $('.js-select2').select2();


//    ////Datepicker
//    //$('.js-datepicker').daterangepicker({
//    //    singleDatePicker: true,
//    //    autoApply: true,
//    //    drops: 'up',
//    //    maxDate: new Date()
//    //});






//    //sweat alert
//    var message = $('#Message').text();
//    if (message !== '') {
//        SuccessAlert(message);
//    }
//    //datatables
//    console.log('Document ready, initializing datatable...');
//    KTDatatableExample.init();

//    //handel modal

//    $('body').delegate('.js-render-modal','click', function () {

//        var btn = $(this);
//        var modal = $('#exampleModal');
//        modal.find('#exampleModalLabel').text(btn.data('title'));

//        if (btn.data('update') !== undefined) {
//            UpdatedRow = btn.closest('tr');
//        }
//        $.get({
//            url: btn.data('url'),

//            success: function (form) {
//                modal.find('#ModalBody').html(form);
//                $.validator.unobtrusive.parse(modal);

//            },
//            error: function () {
//                ErrorAlert();
//            }
//        })
//        modal.modal('show');
//    })
//});


////toggle status

//$(document).ready(function () {


//    // Toggle status functionality with event delegation
//    $(document).on('click', '.js-toggle-status', function (e) {
//        e.preventDefault();
//        var btn = $(this);
//        var categoryId = btn.data('id');

//        if (!categoryId) {
//            console.error('Category ID not found');
//            return;
//        }

//        bootbox.confirm({
//            title: 'Confirm Status Change',
//            message: 'Are you sure you want to toggle the status of this category?',
//            buttons: {
//                confirm: {
//                    label: 'Yes',
//                    className: 'btn-danger'
//                },
//                cancel: {
//                    label: 'No',
//                    className: 'btn-secondary'
//                }
//            },
//            callback: function (result) {
//                if (result) {
//                    $.ajax({
//                        url: btn.data('url'),
//                        type: 'POST',
//                        data: {
//                            '__RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
//                        },
//                        success: function (response) {
//                            var row = btn.closest('tr');
//                            var statusSpan = row.find('.js-status');
//                            var lastUpdateCell = row.find('.js-last-update');

//                            // Update status text and class
//                            var currentStatus = statusSpan.text().trim();
//                            var newStatus = currentStatus === 'Deleted' ? 'Available' : 'Deleted';
//                            statusSpan.text(newStatus);

//                            // Toggle badge classes
//                            if (newStatus === 'Available') {
//                                statusSpan.removeClass('badge-light-danger').addClass('badge-light-success');
//                            } else {
//                                statusSpan.removeClass('badge-light-success').addClass('badge-light-danger');
//                            }

//                            // Update last updated date
//                            if (response.lastUpdatedOn) {
//                                lastUpdateCell.text(response.lastUpdatedOn);
//                            }

//                            // Add flash animation
//                            row.addClass('animate__animated animate__flash');
//                            setTimeout(function () {
//                                row.removeClass('animate__animated animate__flash');
//                            }, 1000);

//                            // Show success message
//                            if (typeof toastr !== 'undefined') {
//                                toastr.success('Category status updated successfully');
//                            } else {
//                                alert('Category status updated successfully');
//                            }
//                        },
//                        error: function (xhr, status, error) {
//                            console.error('Error toggling status:', error);
//                            if (typeof toastr !== 'undefined') {
//                                toastr.error('An error occurred while updating the status');
//                            } else {
//                                alert('An error occurred while updating the status');
//                            }
//                        }
//                    });
//                }
//            }
//        });
//    });
//});



var table;
var datatable;
var UpdatedRow;
var exportedCols = [];

/* =========================
   Alerts
========================= */

function showSuccessMessage(message = 'Saved successfully!') {
    Swal.fire({
        icon: 'success',
        title: 'Success',
        text: message,
        customClass: {
            confirmButton: "btn btn-primary"
        }
    });
}

function showErrorMessage(message = 'Something went wrong!') {
    Swal.fire({
        icon: 'error',
        title: 'Error',
        text: message,
        customClass: {
            confirmButton: "btn btn-primary"
        }
    });
}

/* =========================
   Modal Handlers
========================= */


function DisableSumbitButton() {
    $('body :submit')
        .attr('disabled', 'disabled')
        .attr('data-kt-indicator', 'on');
}
function onModalBegin() {
    DisableSumbitButton();
}

function onModalSuccess(row) {
    showSuccessMessage();

    $('#Modal').modal('hide');

    if (UpdatedRow !== undefined) {
        datatable.row(UpdatedRow).remove().draw();
        UpdatedRow = undefined;
    }

    var newRow = $(row);
    datatable.row.add(newRow).draw();

    KTMenu.init();
    KTMenu.initGlobalHandlers();
}

function onModalComplete() {
    $('body :submit')
        .removeAttr('disabled')
        .removeAttr('data-kt-indicator');
}

/* =========================
   DataTable
========================= */

function initExportColumns() {
    exportedCols = [];

    $('th').each(function (i) {
        if (!$(this).hasClass('js-no-export')) {
            exportedCols.push(i);
        }
    });
}

var KTDatatables = function () {

    var initDatatable = function () {

        table = document.querySelector('.js-datatables');
        if (!table) return;

        datatable = $(table).DataTable({
            info: false,
            pageLength: 10
        });
    };

    var exportButtons = function () {

        if (!table) return;

        const documentTitle = $('.js-datatables').data('document-title') || 'Report';

        var buttons = new $.fn.dataTable.Buttons(table, {
            buttons: [
                {
                    extend: 'copyHtml5',
                    title: documentTitle,
                    exportOptions: { columns: exportedCols }
                },
                {
                    extend: 'excelHtml5',
                    title: documentTitle,
                    exportOptions: { columns: exportedCols }
                },
                {
                    extend: 'csvHtml5',
                    title: documentTitle,
                    exportOptions: { columns: exportedCols }
                },
                {
                    extend: 'pdfHtml5',
                    title: documentTitle,
                    exportOptions: { columns: exportedCols }
                }
            ]
        });

        buttons.container().appendTo($('#kt_datatable_example_buttons'));

        // Hook UI export menu
        const exportLinks = document.querySelectorAll('#kt_datatable_example_export_menu [data-kt-export]');

        exportLinks.forEach(link => {
            link.addEventListener('click', function (e) {
                e.preventDefault();

                const exportValue = this.getAttribute('data-kt-export');
                const target = document.querySelector('.dt-buttons .buttons-' + exportValue);

                if (target) target.click();
            });
        });
    };

    var handleSearch = function () {
        const searchInput = document.querySelector('[data-kt-filter="search"]');

        if (!searchInput || !datatable) return;

        searchInput.addEventListener('keyup', function (e) {
            datatable.search(e.target.value).draw();
        });
    };

    return {
        init: function () {
            initDatatable();
            exportButtons();
            handleSearch();
        }
    };
}();

/* =========================
   Init
========================= */

$(document).ready(function () {
    //disable submit button and appear spiner
    $('form').on('submit', function () {

        if ($('.js-tinymce').length > 0) {
            $('.js-tinymce').each(function () {
                var input = $(this);
                var content = tinymce.get(input.attr('id')).getContent();
                input.val(content);
            });
        }
        var isValid = $(this).valid();
        if (isValid) DisableSumbitButton();
    });

    initExportColumns();

    /* TinyMCE */

    if ($('.js-tinymce').length > 0) {


        var options = {
            selector: ".js-tinymce",
            height: "422"
        };

        if (typeof KTThemeMode !== "undefined" &&
            KTThemeMode.getMode &&
            KTThemeMode.getMode() === "dark") {

            options.skin = "oxide-dark";
            options.content_css = "dark";
        }

        if (typeof tinymce !== "undefined") {
            tinymce.init(options);
        }
    }
   

 
    /* Select2 */
    $('.js-select2').select2();
    $('.js-select2').on('select2:select', function(e){
        var select = $(this);
        $('form').validate().element('#' + select.attr('id'));
    });

    /* Datepicker */
    $('.js-datepicker').daterangepicker({
        singleDatePicker: true,
        autoApply: true,
        drops: 'up',
        maxDate: new Date()
    });

    /* SweetAlert message */
    var message = $('#Message').text();
    if (message && message.trim() !== '') {
        showSuccessMessage(message);
    }

    /* Init datatable */
    KTDatatables.init();

    /* =========================
       Modal load
    ========================= */

    $('body').on('click', '.js-render-modal', function () {

        var btn = $(this);
        var modal = $('#Modal');

        modal.find('#ModalLabel').text(btn.data('title'));

        if (btn.data('update') !== undefined) {
            UpdatedRow = btn.closest('tr');
        }

        $.get({
            url: btn.data('url'),
            success: function (form) {
                modal.find('.modal-body').html(form);
                $.validator.unobtrusive.parse(modal);
            },
            error: function () {
                showErrorMessage();
            }
        });

        modal.modal('show');
    });

    /* =========================
       Toggle Status
    ========================= */

    $('body').on('click', '.js-toggle-status', function () {

        var btn = $(this);

        bootbox.confirm({
            message: "Are you sure you want to change status?",
            buttons: {
                confirm: { label: 'Yes', className: 'btn-danger' },
                cancel: { label: 'No', className: 'btn-secondary' }
            },
            callback: function (result) {

                if (!result) return;

                $.post({
                    url: btn.data('url'),
                    data: {
                        '__RequestVerificationToken':
                            $('input[name="__RequestVerificationToken"]').val()
                    },
                    success: function (lastUpdatedOn) {

                        var row = btn.closest('tr');
                        var status = row.find('.js-status');

                        var newStatus =
                            status.text().trim() === 'Deleted'
                                ? 'Available'
                                : 'Deleted';

                        status.text(newStatus)
                            .toggleClass('badge-light-success badge-light-danger');

                        row.find('.js-updated-on').html(lastUpdatedOn);

                        row.addClass('animate__animated animate__flash');

                        showSuccessMessage();
                    },
                    error: function () {
                        showErrorMessage();
                    }
                });
            }
        });
    });

});