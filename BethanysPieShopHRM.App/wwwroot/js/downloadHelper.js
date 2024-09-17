function downloadImage(base64String, fileName) {
    const link = document.createElement('a');
    link.href = 'data:image/png;base64,' + base64String;
    link.download = fileName;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}
