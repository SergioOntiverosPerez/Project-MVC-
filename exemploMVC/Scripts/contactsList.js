const uriContacts = window.location.origin + "/api/contacts";

console.log(uriContacts);

let contacts = [];
getContacts();

function getContacts() {
    fetch(uriContacts)
        .then(response => response.json())
        .then(data => _displayContacts(data))
        .catch(error => console.error('Unable to get contacts', error));

}

function deleteContact(id) {
    const direccion = uriContacts + `/${id.trim()}`;
    console.log(direccion);
    var result = confirm("Do you want to delete?")
    if (result) {
        fetch(direccion,
            {
                method: 'DELETE'
            }).then(() => getContacts())
            .catch(error => console.error('Unable to delete contact', error));
    }
}

function _displayContactssCount(contactCount) {
    const name = (contactCount == 1) ? 'Contact' : 'Contacts';
    document.getElementById("counter").innerHTML = `${contactCount} ${name}`;
}

function _displayContacts(data) {

    console.log(data);
    const tBody = document.getElementById("contacts");
    tBody.innerHTML = "";

    _displayContactssCount(data.length);

    const button = document.createElement('button');
    const a = document.createElement('a');

    data.forEach(contact => {

        let editButton = a.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.classList.add('btn');
        editButton.classList.add('btn-secondary');
        let urlParam = window.location.origin + "/contacts/edit/" + contact.ContactID;
        editButton.setAttribute('href', urlParam);

        let detailsButton = a.cloneNode(false);
        detailsButton.innerText = 'Details';
        detailsButton.classList.add('btn');
        detailsButton.classList.add('btn-info');
        let urlDetails = window.location.origin + "/contacts/details/" + contact.ContactID;
        detailsButton.setAttribute('href', urlDetails);


        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', 'deleteContact(" ' + contact.ContactID + ' ")');
        deleteButton.classList.add('btn');
        deleteButton.classList.add('btn-danger');


        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        let textNode1 = document.createTextNode(contact.ContactID);
        td1.appendChild(textNode1);

        let td2 = tr.insertCell(1);
        let textNode2 = document.createTextNode(contact.Name);
        td2.appendChild(textNode2);

        let td3 = tr.insertCell(2);
        let textNode3 = document.createTextNode(contact.Email);
        td3.appendChild(textNode3);

        let td5 = tr.insertCell(3);
        td5.appendChild(editButton);

        let td6 = tr.insertCell(4);
        td6.appendChild(detailsButton);

        let td7 = tr.insertCell(5);
        td7.appendChild(deleteButton);
    });
    contacts = data;
}