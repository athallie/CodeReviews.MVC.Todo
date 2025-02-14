// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const url = 'https://localhost:7039'
let todos = []
const modal = new bootstrap.Modal("#addModal")
let modalDOM = document.querySelector("#addModal")
let modalAddButton = document.querySelector("#addButtonModal")

window.addEventListener("DOMContentLoaded", async () => {
    let items = await getItems()
    displayHeaders(items)
    displayItems(items)
})

modalAddButton.addEventListener("click", async () => {
    let title = document.querySelector("#titleField").value
    let description = document.querySelector("#descriptionField").value
    await addItem([title, description])
    modal.hide()
})

modalDOM.addEventListener("hidden.bs.modal", async () => {
    let items = await getItems()
    displayItems(items)
})


async function getItems() {
    try {
        let response = await fetch(`${url}/todos`)
        let data = await response.json()
        return data
    } catch (error) {
        console.error("Unable to get items.", error)
    }
    return false
}

async function addItem(data) {
    data = JSON.stringify({
        title: data[0],
        description: data[1]
    })
    console.log(data)
    await fetch(`${url}/todos`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: data,
    })
        .catch(error => console.log(error))
}

function openAddDataModal() {

}

function displayHeaders(data) {
    let keys = Object.keys(data[0])
    let headerRow = document.querySelector("#dataTableHeader")
    keys.forEach(key => {
        let col = document.createElement("div")
        col.classList.add("col")
        col.textContent = key
        headerRow.appendChild(col)
    })
}

function displayItems(data) {
    let keys = Object.keys(data[0])
    let tableBody = document.querySelector("#dataTableBody")
    tableBody.replaceChildren()
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