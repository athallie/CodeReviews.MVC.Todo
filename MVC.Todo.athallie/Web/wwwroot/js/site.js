// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const url = 'https://localhost:7039'
let todos = []

window.addEventListener("DOMContentLoaded", getItems())

function getItems() {
    fetch(`${url}/todos`)
        .then(response => response.json())
        .then(data => {
            displayHeaders(data)
            _displayItems(data)
        })
        .catch(error => console.error('Unable to get items.', error))
}

function displayHeaders(data) {
    let keys = Object.keys(data[0])
    let headerRow = document.querySelector("#dataTableHeader");
    keys.forEach(key => {
        let col = document.createElement("div")
        col.classList.add("col")
        col.textContent = key
        headerRow.appendChild(col)
    })
}

function _displayItems(data) {
    let keys = Object.keys(data[0])
    let tableBody = document.querySelector("#dataTableBody")
    data.forEach(e => {
        let row = document.createElement("div");
        row.classList.add("row", "dataRows");
        keys.forEach(key => {
            let col = document.createElement("div");
            col.classList.add("col", "valueCells");
            col.textContent = e[key]
            row.appendChild(col)
        })
        tableBody.appendChild(row)
    })
}