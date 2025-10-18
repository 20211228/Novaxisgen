"use strict";
var KTSignupGeneral = function() {
    var e, t, r, a, s = function() {
        return a.getScore() > 50
    };
    return {
        init: function() {
            e = document.querySelector("#kt_sign_up_form"), t = document.querySelector("#kt_sign_up_submit"), a = KTPasswordMeter.getInstance(e.querySelector('[data-kt-password-meter="true"]')), ! function(e) {
                try {
                    return new URL(e), !0
                } catch (e) {
                    return !1
                }
            }(t.closest("form").getAttribute("action")) ? (r = FormValidation.formValidation(e, {
                fields: {
                    "first-name": {
                        validators: {
                            notEmpty: {
                                message: "First Name is required"
                            }
                        }
                    },
                    "last-name": {
                        validators: {
                            notEmpty: {
                                message: "Last Name is required"
                            }
                        }
                    },
                    email: {
                        validators: {
                            regexp: {
                                regexp: /^[^\s@]+@[^\s@]+\.[^\s@]+$/,
                                message: "The value is not a valid email address"
                            },
                            notEmpty: {
                                message: "Email address is required"
                            }
                        }
                    },
                    login: {
						validators: {
							notEmpty: {
								message: 'Username is required.'
							},
							stringLength: {
								min: 4,
								max: 15,
								message: 'The username has to be between 4 and 15 characters long.',
							}
						}
					},
                    password: {
                        validators: {
                            notEmpty: {
                                message: "The password is required"
                            },
							stringLength: {
								min: 6,
								max: 25,
								message: 'The password has to be between 6 and 25 characters long.',
							},
                            callback: {
                                message: "Please enter valid password",
                                callback: function(e) {
                                    if (e.value.length > 0) return s()
                                }
                            }
                        }
                    },
                    "confirm-password": {
                        validators: {
                            notEmpty: {
                                message: "The password confirmation is required"
                            },
                            identical: {
                                compare: function() {
                                    return e.querySelector('[name="password"]').value
                                },
                                message: "The password and its confirm are not the same"
                            }
                        }
                    },
                    wallet_address: {
                        validators: {
                            notEmpty: {
                                message: "Wallet Address is required"
                            },
                            stringLength: {
                                min: 10,
                                max: 100,
                                message: "Wallet Address must be between 10 and 100 characters"
                            },
                            regexp: {
  regexp: /^\s*(0x)?[0-9a-fA-F]{40}\s*$/,
  message: "Wallet Address must be a valid USDT (BEP20) address"
}
                        }
                    },
                    toc: {
                        validators: {
                            notEmpty: {
                                message: "You must accept the terms and conditions"
                            }
                        }
                    }
                },
                plugins: {
                    trigger: new FormValidation.plugins.Trigger({
                        event: {
                            password: !1
                        }
                    }),
                    bootstrap: new FormValidation.plugins.Bootstrap5({
                        rowSelector: ".fv-row",
                        eleInvalidClass: "",
                        eleValidClass: ""
                    })
                }
            }), t.addEventListener("click", (function(s) {
                s.preventDefault(), r.revalidateField("password"), r.validate().then((function(r) {
                    "Valid" == r ? (t.setAttribute("data-kt-indicator", "on"), t.disabled = !0, setTimeout((function() {
                        $.ajax({
                            type: 'POST',
                            url: '/register',
                            data: $('#kt_sign_up_form').serialize(),
                            success: function(response) {
                                // $(btn).attr("disabled", false);
                                // KTApp.unprogress(btn);
                                //KTApp.unblock(_formEl);
                                
                                
                                console.log(response);
            
                                if (response.code == "#registered") {
                                    t.removeAttribute("data-kt-indicator"), t.disabled = !1, Swal.fire({
                                        text: "Congratulations, you are the newest member of Genesis, log in to your account!",
                                        icon: "success",
                                        buttonsStyling: !1,
                                        confirmButtonText: "Sign In now!",
                                        customClass: {
                                            confirmButton: "btn btn-primary"
                                        }
                                    }).then((function(t) {
                                        if (t.isConfirmed) {
                                            e.reset(), a.reset();
                                            var r = e.getAttribute("data-kt-redirect-url");
                                            r && (location.href = r)
                                        }
                                    }))
                                } else {
                                    swal.fire({
                                        "title": ":(", 
                                        "text": "Error creating your account, please try again.", 
                                        "type": "warning",
                                        "showCloseButton": true,
                                        "confirmButtonClass": "btn btn-secondary"
                                    });
                                }
                            },
                            error: function(response) {
                                // $(btn).attr("disabled", false);
                                // var myJSON = JSON.stringify(response);
                                // console.log(myJSON);
                                // console.log(response['responseJSON']['errors']);
                                KTUtil.scrollTop();
                                $.each(response['responseJSON']['errors'], function(index, value) {
                                    swal.fire({
                                        "title": ":(", 
                                        "text": value[0], 
                                        "type": "warning",
                                        "showCloseButton": true,
                                        "confirmButtonClass": "btn btn-secondary"
                                    });
                                });
                                t.removeAttribute("data-kt-indicator"), t.disabled = !1;
                            }
                        });
                    }), 1500)) : Swal.fire({
                        text: "Sorry, looks like there are some errors detected, please try again.",
                        icon: "error",
                        buttonsStyling: !1,
                        confirmButtonText: "Ok, got it!",
                        customClass: {
                            confirmButton: "btn btn-primary"
                        }
                    })
                }))
            })), e.querySelector('input[name="password"]').addEventListener("input", (function() {
                this.value.length > 0 && r.updateFieldStatus("password", "NotValidated")
            }))) : (r = FormValidation.formValidation(e, {
                fields: {
                    login: {
                        validators: {
							notEmpty: {
								message: 'Username is required'
							},
							stringLength: {
								min: 4,
								max: 15,
								message: 'The username has to be between 4 and 15 characters long.',
							}
						}
                    },
                    email: {
                        validators: {
                            regexp: {
                                regexp: /^[^\s@]+@[^\s@]+\.[^\s@]+$/,
                                message: "The value is not a valid email address"
                            },
                            notEmpty: {
                                message: "Email address is required"
                            }
                        }
                    },
                    password: {
                        validators: {
                            notEmpty: {
                                message: "The password is required"
                            },
							stringLength: {
								min: 6,
								max: 25,
								message: 'The password has to be between 6 and 25 characters long.',
							},
                            callback: {
                                message: "Please enter valid password",
                                callback: function(e) {
                                    if (e.value.length > 0) return s()
                                }
                            }
                        }
                    },
                    password_confirmation: {
                        validators: {
                            notEmpty: {
                                message: "The password confirmation is required"
                            },
                            identical: {
                                compare: function() {
                                    return e.querySelector('[name="password"]').value
                                },
                                message: "The password and its confirm are not the same"
                            }
                        }
                    },
                    toc: {
                        validators: {
                            notEmpty: {
                                message: "You must accept the terms and conditions"
                            }
                        }
                    }
                },
                plugins: {
                    trigger: new FormValidation.plugins.Trigger({
                        event: {
                            password: !1
                        }
                    }),
                    bootstrap: new FormValidation.plugins.Bootstrap5({
                        rowSelector: ".fv-row",
                        eleInvalidClass: "",
                        eleValidClass: ""
                    })
                }
            }), t.addEventListener("click", (function(a) {
                a.preventDefault(), r.revalidateField("password"), r.validate().then((function(r) {
                    "Valid" == r ? (t.setAttribute("data-kt-indicator", "on"), t.disabled = !0, axios.post(t.closest("form").getAttribute("action"), new FormData(e)).then((function(t) {
                        if (t) {
                            e.reset();
                            const t = e.getAttribute("data-kt-redirect-url");
                            t && (location.href = t)
                        } else Swal.fire({
                            text: "Sorry, looks like there are some errors detected, please try again.",
                            icon: "error",
                            buttonsStyling: !1,
                            confirmButtonText: "Ok, got it!",
                            customClass: {
                                confirmButton: "btn btn-primary"
                            }
                        })
                    })).catch((function(e) {
                        Swal.fire({
                            text: "Sorry, looks like there are some errors detected, please try again.",
                            icon: "error",
                            buttonsStyling: !1,
                            confirmButtonText: "Ok, got it!",
                            customClass: {
                                confirmButton: "btn btn-primary"
                            }
                        })
                    })).then((() => {
                        t.removeAttribute("data-kt-indicator"), t.disabled = !1
                    }))) : Swal.fire({
                        text: "Sorry, looks like there are some errors detected, please try again.",
                        icon: "error",
                        buttonsStyling: !1,
                        confirmButtonText: "Ok, got it!",
                        customClass: {
                            confirmButton: "btn btn-primary"
                        }
                    })
                }))
            })), e.querySelector('input[name="password"]').addEventListener("input", (function() {
                this.value.length > 0 && r.updateFieldStatus("password", "NotValidated")
            })))
        }
    }
}();
KTUtil.onDOMContentLoaded((function() {
    KTSignupGeneral.init()
}));