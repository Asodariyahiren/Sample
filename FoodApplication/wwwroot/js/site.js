
let apiURl = "https://forkify-api.herokuapp.com/api/v2/recipes";
let apiKey = "568414a2-7b45-48bb-ba5b-cdc5b22f3f6c";

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