export function FormatDate(date: Date) {
  console.log()

    var months = ["jaanuar", "veebruar", "märts", "aprill", "mai", "juuni", "juuli", "august", "september", "oktoober", "november", "detsember"]
    var d = new Date(date);

    var min = d.getMinutes().toString();
    if(d.getMinutes().toString().length === 1){
      min = "0" + min 
    }
    return ("" + d.getDate() + "." + months[d.getMonth()] + " " + d.getHours() + ":" + min)
  }