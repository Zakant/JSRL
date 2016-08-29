var id = startThread(function () {
    log('register for ipc now!');
    var id = ipc.register('test', function (data) {
        log('Thread B logs: ' + data);
    });
    log('Registered with id: ' + id);
    while (true) {
        log('do work in thread');
        delay(1000);
    }
});
delay(2000);
ipc.send('test', 'Hello from Thread A');
delay(2000);
stopThread(id);