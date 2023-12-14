
export async function GetContacts(UserName){
    const response = await fetch(`fetchdata/getContacts/${UserName}`, {
        method: 'GET'
    });
    const data = response.json();

    let res;
    await data.then(result => {
        res = result;
    });
    return res;
}

export async function GetMessages(UserName, ReceiverName){
    const response = await fetch(`fetchdata/getMessages/${UserName}/${ReceiverName}`, {
        method: 'GET'
    });
}


export async function GetCurrentUser(){
    const response = await fetch(`fetchdata/getCurrentUser`, {
        method: 'GET'
    });
    const data = response.json();

    let res;
    await data.then(result => {
        res = result;
        console.log(data);
    });
    return res;
}

export async function Listen(messages){
//    let x = setInterval(async () => {
//     const response = await fetch(`fetchdata/getMessages`, {
//         method: 'GET'
//     });
//    }, 50);
   
}