
$("#autocomplete").autocomplete({
    minLength: 2,
    delay: 500,
    source: "adm/Produto/buscar",
    select: function (event, ui) {
        $('#Produto_ProdutoId').attr('value', ui.item.id);
        $('#Produto_Nome').attr('value', ui.item.label);
    }
});

//ajax imagem GET

$(document).ready(function () {
    var produtoOrdem = $('#listaImagens').data("produtoordem");
    var url = "adm/Produto/MostrarFotosAjax";

    $.ajax({
        url: url
        , type: "GET"
        , data: { produtoOrdem: produtoOrdem }
        , datatype: "json"
        , success: function (data) {

            var divItens = $("#listaImagens");
            divItens.empty();
            divItens.show();
            divItens.html(data);
        }
    });
});

// file upload

function goBack() {
    window.history.back();
}