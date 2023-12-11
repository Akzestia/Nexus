




// console.log(data + " data");
// var isDark = data;

// console.log("Init " + isDark);
const r = document.querySelector(":root");

export async function SetTheme(){
 
  var x = await GetThemeStatus();
  console.log(x + " x-value");
  if(!x){
    r.style.setProperty("--theme-color-border", "#373e47");
    r.style.setProperty("--theme-color-font", "black");
    r.style.setProperty("--theme-color-background", "white");
    r.style.setProperty("--theme-color-border-focus-i", "aqua");
    r.style.setProperty("--theme-color-border-i", "#373e47");
    r.style.setProperty("--theme-color-font-icon", "black");
  }
  else{
    r.style.setProperty("--theme-color-border", "#373e47");
    r.style.setProperty("--theme-color-font", "#8300ff");
    r.style.setProperty("--theme-color-background", "black");
    r.style.setProperty("--theme-color-border-focus-i", "#8300ff");
    r.style.setProperty("--theme-color-border-i", "#373e47");
    r.style.setProperty("--theme-color-font-icon", "white");
  }
}

export async function GetThemeStatus(){
  const response = await fetch('darkmode/get', { method: "GET" });
  const data = await response.json();
  return data;
}

export async function DarkLightTheme() {
  var isDark = await GetThemeStatus();

  if (isDark) {
    r.style.setProperty("--theme-color-border", "#373e47");
    r.style.setProperty("--theme-color-font", "black");
    r.style.setProperty("--theme-color-background", "white");
    r.style.setProperty("--theme-color-border-focus-i", "aqua");
    r.style.setProperty("--theme-color-border-i", "#373e47");
    r.style.setProperty("--theme-color-font-icon", "black");
    const response = await fetch(`darkmode/toggle/${false}`, { method: "POST"});
    const data = await response.json();
    isDark = data;
    console.log(isDark + " light-x");
    return;
  }

  r.style.setProperty("--theme-color-border", "#373e47");
  r.style.setProperty("--theme-color-font", "#8300ff");
  r.style.setProperty("--theme-color-background", "black");
  r.style.setProperty("--theme-color-border-focus-i", "#8300ff");
  r.style.setProperty("--theme-color-border-i", "#373e47");
  r.style.setProperty("--theme-color-font-icon", "white");
  const response = await fetch(`darkmode/toggle/${true}`, { method: "POST"});
  const data = await response.json();
  isDark = data;
  console.log(isDark + " dark-x");
}
