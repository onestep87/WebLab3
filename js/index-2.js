document.addEventListener('DOMContentLoaded', onloadDropdown);

async function onloadDropdown(){
    await downloadDropdownFromDB(localStorage.getItem('dropdown'));
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