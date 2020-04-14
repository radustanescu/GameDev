Proxy = (function () {
    var getUrl = window.location;
    var baseUrl = getUrl.protocol + "//" + getUrl.host + "/" + getUrl.pathname.split('/')[1];

    var callAjax = function (serviceUrl, myData, callback, type, dataType) {
        type = type || "GET";
        if (type == 'POST')
            myData = JSON.stringify(myData);

        try {
            $.ajax({
                url: serviceUrl,
                data: myData,
                timeout: 100000,
                dataType: dataType || "json",
                cache: false,
                type: type,
                contentType: "application/json; charset=utf-8",
                processData: !type || type == 'GET',
                success: function (response) {
                    if (callback)
                        callback(response);
                },
                error: function (jqXhr, textStatus, errorThrown) {
                    if (callback)
                        callback({ Exception: { Message: textStatus } });
                },
                beforeSend: function (jqXhr) {
                    jqXhr.setRequestHeader("Cache-Control", "no-cache");
                    jqXhr.setRequestHeader("pragma", "no-cache");
                }
            });
        } catch (err) {
        }
    };

    return {
        Account: {
            SetUser: function (userJSON, password, callback) {
                var data = {
                    userJSON: userJSON,
                    password: password
                };

                callAjax(baseUrl + "/User/SetUser", data, callback, 'POST');
            }
        },
        Class: {
            CreateCharacter: function (classID) {
                var data = {
                    classID: classID
                };

                callAjax(baseUrl + "/Class/CreateCharacter", data, null, 'POST');
            }
        }
    };
})();