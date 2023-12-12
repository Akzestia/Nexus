


export function SetScroll(){
    const contacts = document.querySelector('.contacts-x-div');

    contacts.addEventListener('scroll', function() {
        contacts.classList.add('scrolling');
      
        clearTimeout(contacts.dataScrollTimer);
        contacts.dataScrollTimer = setTimeout(function() {
            contacts.classList.remove('scrolling');
        }, 50);
    });
    

    const textarea = document.getElementById('message-n-input');

    
    textarea.addEventListener('scroll', function() {
        textarea.classList.add('scrolling');
      
        clearTimeout(textarea.dataScrollTimer);
        textarea.dataScrollTimer = setTimeout(function() {
            textarea.classList.remove('scrolling');
        }, 50);
    });
}


export function SetMessageInput(){
    const textarea = document.getElementById('message-n-input');
    
    textarea.addEventListener('input', function () {
      this.style.height = 'auto'; 
      this.style.height = this.scrollHeight-15.5 + 'px'; 
    });
    
}