let qrcode = new QRCode(document.getElementById("qrArea"), {
    width: 300,
    height: 300
});

function makeCode() {
    let elText = document.getElementById("generatedLink");

    if (!elText.value) {
        alert("Input a text");
        elText.focus();
        return;
    }

    qrcode.makeCode(elText.value);
}

makeCode();

function copyToClipBoard() {
    let copyText = document.getElementById("generatedLink");
    copyText.select();
    copyText.setSelectionRange(0, 99999)
    document.execCommand("copy");
    alert("Link Copied to ClipBoard");
}

let btnDownload = document.getElementById('downloadBtn');
let img = document.querySelector('img');

btnDownload.addEventListener('click', () => {
    let imagePath = img.getAttribute('src');
    saveAs(imagePath, 'QR_code.png');
});


$(document).ready(function () {

    pageLoadCheckValue()


    $("#AppleInput").on("input", function () {
        valueControl($(this), "https://apps.apple.com/")
    })
    $("#GoogleInput").on("input", function () {
        valueControl($(this), "https://play.google.com/store/apps/")
    })
    $("#WebInput").on("input", function () {
        valueControl($(this), ".")
    })


    function updateData(element) {
        data = {
            Appstore: $("#MainAppleInput").val().trim(),
            GooglePlay: $("#MainGoogleInput").val().trim(),
            Web: $("#MainWebInput").val().trim()
        }
        $.ajax({
            url: 'home/updateUrls',
            type: "post",
            contentType: 'application/x-www-form-urlencoded',
            data: data,
            success: function (data) {
                if (data) {
                    if ($(element).val() != "") {
                        $(element).parent().children(".fa-times").fadeOut()
                        $(element).parent().children(".fa-check").fadeIn()
                    }
                }
                else {
                    $(element).parent().children(".fa-check").fadeOut()
                    $(element).parent().children(".fa-times").fadeIn()
                }
            },
            error: function () {
                alert("error")
            }
        });
    }

    function valueControl(element, contains) {
        if ($(element).val() != "") {
            if ($(element).val().includes(contains)) {
                if ($(element).attr("id") == "WebInput")
                {
                    if (!$(element).val().includes("https://")&&!$(element).val().includes("http://"))
                    {
                        $("#Main" + $(element).attr("id")).val("http://"+$(element).val())
                    }
                    else {
                        $("#Main" + $(element).attr("id")).val($(element).val())
                    }
                }
                else
                {                 
                    $("#Main" + $(element).attr("id")).val($(element).val())
                }
                updateData($(element))
            }
            else {
                $("#Main" + $(element).attr("id")).val("")
                $(element).parent().children(".fa-check").fadeOut()
                $(element).parent().children(".fa-times").fadeIn()
            }
        }
        else {
            $("#Main" + $(element).attr("id")).val("")
            $(element).parent().children(".fa-check").fadeOut()
            $(element).parent().children(".fa-times").fadeOut()
            updateData($(element))
        }
    }

    function pageLoadCheckValue() {
        if ($("#AppleInput").val() != "") {
            $("#AppleInput").parent().children(".fa-check").show()
        }

        if ($("#GoogleInput").val() != "") {
            $("#GoogleInput").parent().children(".fa-check").show()
        }

        if ($("#WebInput").val() != "") {
            $("#WebInput").parent().children(".fa-check").show()
        }
    }

})