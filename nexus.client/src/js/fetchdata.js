
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
    const data = await response.json();

    console.log(data);

    return data;
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

export async function Listen(messages, setMessages){
   let x = setInterval(async () => {
    console.log('Waiting for messegaes');
    const response = await fetch(`fetchdata/getMessages`, {
        method: 'GET'
    });

    const data = await response.json();
    console.log(data);
    setMessages(data);
   }, 1000);
   
}