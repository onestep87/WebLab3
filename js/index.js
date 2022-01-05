// Globals
let addButton = document.querySelector('.addMenuPoint');
addButton.addEventListener('click', addLink);

let saveButton = document.querySelector('.saveMenuPoint');
saveButton.addEventListener('click', saveDropdownToDB);

let deleteButton = document.querySelector('.deleteMenuPoint');
deleteButton.addEventListener('click', deleteDropdown);

let select = document.querySelector('.dropdown__select');

document.addEventListener('DOMContentLoaded', onloadDropdown);
document.addEventListener('DOMContentLoaded', onloadAmountDropdown);



// Functions-handlers
function addLink(){
    let dropdown = document.querySelector('.dropdown-content');

    let links = document.querySelectorAll('.dropdown-content > a');

    if(links.length > 9){
        alert('Why do you need so many links?')
    }
    else{
        let input = document.querySelector('.dropdown-input').value;

        if(input != ''){
            let link = document.createElement('a');
            link.innerHTML = input;
            dropdown.append(link);

            console.log('Links in dropdown: ' + links.length+1);
        }
    }  
}

async function saveDropdownToDB(){
    let links = document.querySelectorAll('.dropdown-content > a');

    let url = 'https://localhost:44353/api/Dropdown/create';

    let data = [];

    for(let i = 0; i<links.length; i++){
        data.push(links[i].textContent);
    }

    let response = await fetch(url, {
        method: 'POST',
        headers: {
            "Content-Type": "application/json",
            "Access-Control-Allow-Origin": "https://localhost:44353"
        },
        body: JSON.stringify(data)
    });
}

function changeSelect(){
    let opt = select.options;
    let sel = select.selectedIndex;
    let number = opt[sel].value;

    localStorage.setItem('dropdown', number);

    downloadDropdownFromDB(number);
}

async function downloadDropdownFromDB(number){
    let url = `https://localhost:44353/api/Dropdown/get?number=${number}`;
    let response = await fetch(url, {
        method: 'GET',
        headers: {
            "Content-Type": "application/json",
            "Access-Control-Allow-Origin": "https://localhost:44353"
        }
    });

    let arr = await response.json();

    let dropdown = document.querySelector('.dropdown-content');

    let links = document.querySelectorAll('.dropdown-content > a');
    if(links.length == 0){
        for(let i = 0; i < arr.length; i++){
            let link = document.createElement('a');
            link.innerHTML = arr[i];
            dropdown.append(link);
        }
    }
    else{
        for(let i = 0; i < links.length; i++){
            links[i].remove();
        }

        for(let i = 0; i < arr.length; i++){
            let link = document.createElement('a');
            link.innerHTML = arr[i];
            dropdown.append(link);
        }
    }
}

function deleteDropdown(){
        let input = document.querySelector('.deleteInput').value;

        if(input == ''){
            let link = document.querySelectorAll('.dropdown-content > a');
            for(let i = 0; i < link.length; i++){
                link[i].remove();
            }
        }
        else{
            deleteDropdownFromDB(input);
        }
}

async function deleteDropdownFromDB(number){
    let url = `https://localhost:44353/api/Dropdown/delete?number=${number}`;
    let response = await fetch(url, {
        method: 'DELETE',
        headers: {
            "Content-Type": "application/json",
            "Access-Control-Allow-Origin": "https://localhost:44353"
        }
    });
}

async function onloadDropdown(){
    await downloadDropdownFromDB(localStorage.getItem('dropdown'));
}

async function onloadAmountDropdown(){
    let url = `https://localhost:44353/api/Dropdown/getNumbers`;

    let response = await fetch(url, {
        method: 'GET',
        headers: {
            "Content-Type": "application/json",
            "Access-Control-Allow-Origin": "https://localhost:44353"
        }
    });

    let arr = await response.json();
    
    let val = localStorage.getItem('dropdown');

    for(let i = 0; i < arr.length; i++){
        let option = document.createElement('option');
        option.innerHTML = arr[i];
        option.setAttribute('value', arr[i]);
        if(val < arr.length-1 && i+1 == val)
        option.setAttribute('selected', 'selected');
        else if(val > arr.length-1 && i == arr.length-1)
        option.setAttribute('selected', 'selected');

        select.append(option);
    }

    select.addEventListener('change', changeSelect)
}