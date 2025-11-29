// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $("#calories-form #Unit").trigger("change");
})
$("#calories-form #Unit").on("change", function () {
    if(this.value === "0") {
        $("#calories-form #CaloriesPerUnit").attr("readonly", true).val("");
        $("#calories-form #Amount").attr("readonly", true).val("");
        $("#calories-form #Calories").removeAttr("readonly");
        $("#calories-form #AmountUnit").text("");
    }
    else if(this.value === "1") {
        $("#calories-form #CaloriesPerUnit").removeAttr("readonly");
        $("#calories-form #Amount").removeAttr("readonly")
        $("#calories-form #Calories").attr("readonly", true);
        $("#calories-form #AmountUnit").text("(g)");
    }
    else if(this.value === "2") {
        $("#calories-form #CaloriesPerUnit").removeAttr("readonly");
        $("#calories-form #Amount").removeAttr("readonly")
        $("#calories-form #Calories").attr("readonly", true);
        $("#calories-form #AmountUnit").text("(ml)");
    }
});

$("#calories-form input").on("input", function () {
    if ($("#calories-form #Unit").val() !== "0") {
        const caloriesPerUnit = $("#calories-form #CaloriesPerUnit").val();
        const amount = $("#calories-form #Amount").val();
        $("#calories-form #Calories").val(caloriesPerUnit * amount / 100);
    }
})