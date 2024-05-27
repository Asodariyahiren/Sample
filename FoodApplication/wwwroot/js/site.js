
let apiURl = "https://forkify-api.herokuapp.com/api/v2/recipes";
let apiKey = "8ccf21b3-ec84-4578-92eb-ba1a7d0676f3";

async function GetRecipes(recipeName, id, isAllshow) {
    let resp = await fetch(`${apiURl}?search=${recipeName}&key=${apiKey}`);
    let result = await resp.json();
    let showRes = isAllshow ? result.data.recipes : result.data.recipes.slice(1, 7);
    showRecipes(showRes, id);
}

function showRecipes(recipes, id) {
    $.ajax({
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        type: "POST",
        url: "/Recipe/GetRecipeCard",
        data: JSON.stringify(recipes),
        success: function (htmlResult) {
            $("#" + id).html(htmlResult);

        }
    })
}
async function getOrderRecipe(id, showId) {
    let resp = await fetch(`${apiURl}/${id}?key=${apiKey}`);
    let result = await resp.json();
    let recipe = result.data.recipe;
    showOrderRecipeDetails(recipe, showId);
}

function showOrderRecipeDetails(data, showId) {
    console.log(data)
    $.ajax({
        dataType: "html",
        type: "POST",
        url: "/Recipe/ShowOrder",
        data: data,
        success: function (htmlResult) {
            $("#" + showId).html(htmlResult);

        }
    })
}
//order page
function quantity(option) {
    let qty = $('#qty').val();
    let price = parseInt($('#price').val());
    let totalAmount = 0;
    if (option === 'inc') {
        qty = parseInt(qty) + 1;
    } else {
        qty = qty == 1 ? qty : qty - 1;
    }
    totalAmount = price * qty;
    $('#qty').val(qty);
    $('#totalAmount').val(totalAmount);
}
//Add Cart

async function cart() {
    let iTag = $(this).children('i')[0];
    let recipeId = $(this).attr("data-recipeId");
    if ($(iTag).hasClass('fa-regular')) {
        let resp = await fetch(`${apiURl}/${recipeId}?key=${apiKey}`);
        let result = await resp.json();
        debugger;
        let cart = result.data.recipe;
        cart.RecipeId = recipeId;
        delete cart.id;
        cardRequest(cart, "SaveCart");
    } else {

    }
}

function cardRequest(data, action) {
    $.ajax({
        url:'/Cart/' + action,
        type: 'POST',
        data: data,
        success: function (resp) {
            console.log(resp);
        },
        error: function (err) {
            console.log(err);
        }
    });
}