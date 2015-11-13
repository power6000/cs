
$(function () {
    $('#addUserForm').hide();
    $('#editUserForm').hide();

    $("#showFormAddUser").on('click', function () {
        $('#editUserForm').hide();
        $('#addUserForm').fadeIn();
    });

    $(".deleteUserLink").on('click', function () {

        var id = $(this).data('id');

        if (confirm("Delete User?")) {
            $.ajax({
                url: 'http://localhost:7738/Users/DeleteUser',
                type: "POST",
                dataType: 'json',
                data: { idParam: id },
                success:
                    function (data) {
                        $("tr#userRow_" + data.id).fadeOut(1000);
                        alert(data.message);
                    }
            });
        };
    });

    $(".editUserLink").on('click', function (even) {
        $('#addUserForm').hide();
        $('#editUserForm').fadeIn();

        var id = $(this).data('id');
        $("#editUserForm .js-Id").val(id);

        $.ajax({
            url: 'http://localhost:7738/Users/EditUserQuery',
            type: "POST",
            dataType: 'json',
            data: { idParam: id },
            success:
                function (data) {
                    $('#editUserForm .js-firstNameEdit').val(data.firstName);
                    $('#editUserForm .js-lastNameEdit').val(data.lastName);
                    $('#editUserForm .js-eMailEdit').val(data.eMail);
                }
        });

        $('#editUserForm .okEdit').on('click', function () {
            return confirm('Edit User?');
        });

    });

});

function EditUser(data) {
    alert('User changed');

    $('#editUserForm').fadeOut();
}
