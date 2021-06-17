<template>
  <div class="container">
    <br />
    <p class="text-center">Saloot Project Signin page</p>
    <hr />

    <div class="row justify-content-center">
      <div class="col-md-6">
        <div class="card">
          <header class="card-header">
            <a
              class="float-right btn btn-outline-primary mt-1"
              v-if="authMode == 'Login'"
              @click="formSwitching()"
              >Register</a
            >
            <a
              class="float-right btn btn-outline-primary mt-1"
              v-else
              @click="formSwitching()"
              >Login</a
            >
            <h4 class="card-title mt-2">{{ authMode }}</h4>
          </header>
          <article class="card-body">
            <form v-if="authMode == 'Login'">
              <div class="form-row">
                <div class="col form-group">
                  <label>Username </label>
                  <input
                    type="text"
                    v-model="login.username"
                    class="form-control"
                    @change="checkUsername()"
                    placeholder=""
                  />
                  <small class="validation">{{
                    validations.loginUsername
                  }}</small>
                </div>
              </div>

              <div class="form-group">
                <label>Password</label>
                <input
                  type="password"
                  v-model="login.password"
                  class="form-control"
                />
              </div>
            </form>

            <form v-if="authMode == 'Register'">
              <div class="form-row">
                <div class="col form-group">
                  <label>Username </label>
                  <input
                    type="text"
                    v-model="register.username"
                    @change="checkUsername()"
                    class="form-control"
                    placeholder=""
                  />
                  <small class="validation">{{
                    validations.registerUsername
                  }}</small>
                </div>

                <div class="col form-group">
                  <label>Password</label>
                  <input
                    type="password"
                    v-model="register.password"
                    @change="checkPassword()"
                    class="form-control"
                  />
                  <small class="validation">{{
                    validations.registerPassword
                  }}</small>
                </div>
              </div>

              <div class="form-row">
                <div class="col form-group">
                  <label>Firstname </label>
                  <input
                    type="text"
                    v-model="register.firstname"
                    @change="checkFirstname()"
                    class="form-control"
                    placeholder=""
                  />
                  <small class="validation">{{ validations.firstname }}</small>
                </div>

                <div class="col form-group">
                  <label>Lastname</label>
                  <input
                    type="text"
                    v-model="register.lastname"
                    @change="checkLastname()"
                    class="form-control"
                  />
                  <small class="validation">{{ validations.lastname }}</small>
                </div>
              </div>

              <div class="form-row">
                <div class="col form-group">
                  <label>Email </label>
                  <input
                    type="text"
                    v-model="register.email"
                    @change="checkEmail()"
                    class="form-control"
                    placeholder=""
                  />
                  <small class="validation">{{ validations.email }}</small>
                </div>

                <div class="col form-group">
                  <label>Phone Number</label>
                  <input
                    type="text"
                    v-model="register.phoneNumber"
                    @change="checkPhonenumber()"
                    class="form-control"
                  />
                  <small class="validation">{{
                    validations.phoneNumber
                  }}</small>
                </div>
              </div>

              <div class="form-row">
                <div class="col form-group">
                  <label>Birthdate </label>
                  <input
                    type="date"
                    v-model="register.birthdate"
                    class="form-control"
                    placeholder=""
                  />
                </div>

                <div class="col form-group">
                  <label>Gender</label>
                  <div>
                    <div class="form-check form-check-inline">
                      <label class="form-check-label" for="inlineRadioMale"
                        >Male</label
                      >
                      <input
                        id="inlineRadioMale"
                        type="radio"
                        name="gender"
                        value="1"
                        v-model="register.gender"
                        class="form-check-input"
                        checked
                      />
                    </div>

                    <div class="form-check form-check-inline">
                      <label class="form-check-label" for="inlineRadioFemale"
                        >Female</label
                      >
                      <input
                        id="inlineRadioFemale"
                        type="radio"
                        name="gender"
                        value="2"
                        v-model="register.gender"
                        class="form-check-input"
                      />
                    </div>
                  </div>
                </div>
              </div>
            </form>

            <div class="form-group">
              <button
                type="submit"
                @click="authorizeAction()"
                class="btn btn-primary btn-block"
              >
                {{ btnSignInTxt }}
              </button>
            </div>
          </article>

          <div class="border-top card-body text-center">
            {{ footerAnchorTagTxt }}
            <a
              v-if="authMode == 'Login'"
              @click="formSwitching()"
              class="text-decoration : underline"
              >Register</a
            >
            <a v-else @click="formSwitching()">Login</a>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import Login from "@/models/user/login";
import { UserCreate } from "@/models/user/user";
import userModule from "@/store/modules/user/userModule";
import validationUtils from "@/common/validationUtils";
import { notifySuccess, notifyError } from "@/common/notificationUtils";
import AuthMode from "@/common/Enums/authMode";
import router from "@/router";
import routesNames from "@/router/routesNames";
import GenderType from "@/common/Enums/genderType";
import { isNullOrEmpty } from "@/common/stringUtils";

@Component
export default class Authentication extends Vue {
  private authMode = AuthMode.Login;
  private btnSignInTxt = "Login";
  private footerAnchorTagTxt = "Don't have an account? Sign up here";
  
  private login: Login = {
    username: "",
    password: ""
  };

  private register: UserCreate = {
    username: "",
    password: "",
    firstname: "",
    lastname: "",
    email: "",
    birthdate: "",
    phoneNumber: "",
    gender: GenderType.Male,
    teamId: null,
    profilePictureId: null
  };

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  private validations: any = {
    loginUsername: "",
    registerUsername: "",
    registerPassword: "",
    firstname: "",
    lastname: "",
    email: "",
    phoneNumber: "",
  };

  formSwitching(): void {
    this.authMode =
      this.authMode == AuthMode.Login ? AuthMode.Register : AuthMode.Login;
    if (this.authMode == AuthMode.Login) {
      this.btnSignInTxt = "Login";
      this.footerAnchorTagTxt = "Don't have an account? Sign up here";
    } else {
      this.btnSignInTxt = "Sign up";
      this.footerAnchorTagTxt = "Already have an account? Sign in here";
    }
  }

  checkUsername(): void {
    switch (this.authMode) {
      case AuthMode.Login:
        if (!validationUtils.isValidUsername(this.login.username)) {
          this.validations.loginUsername = "Username is invalid";
        } else {
          this.validations.loginUsername = "";
        }
        break;

      case AuthMode.Register:
        if (!validationUtils.isValidUsername(this.register.username)) {
          this.validations.registerUsername = "Username is invalid";
        }
        else if (this.register.username.length == 0) {
          this.validations.registerUsername = "Username is invalid";
        }
        else if (this.register.username.length > 15) {
          this.validations.registerUsername = "Username should be less than 15 characters";
        }
        else {
          this.validations.registerUsername = "";
        }
        break;
    }
  }

  checkEmail(): void {
    if (!validationUtils.isValidEmail(this.register.email)) {
      this.validations.email = "Email is invalid";
    } else {
      this.validations.email = "";
    }
  }

  checkFirstname(): void {
    if (this.register.firstname.length == 0) {
      this.validations.firstname = "Firstname is invalid";
    }
    else if (this.register.firstname.length > 35) {
      this.validations.firstname = "Firstname should be less than 35 characters";
    }
    else {
      this.validations.firstname = "";
    }
  }

  checkLastname(): void {
    if (this.register.lastname.length == 0) {
      this.validations.lastname = "Lastname is invalid";
    }
    else if (this.register.firstname.length > 35) {
      this.validations.lastname = "Lastname should be less than 35 characters";
    }
    else {
      this.validations.lastname = "";
    }
  }

  checkPassword(): void {
    if (this.register.password.length == 0) {
      this.validations.registerPassword = "Password is necessary";
    }
    else if (this.register.password.length > 127) {
      this.validations.registerPassword = "Password should be less than 127 characters";
    }
    else {
      this.validations.registerPassword = "";
    }
  }

  checkPhonenumber(): void {
    if (this.register.phoneNumber.length == 0) {
      this.validations.phoneNumber = "Phone Number is invalid";
    }
    else {
      this.validations.phoneNumber = "";
    }
  }

  ValidateForm(): boolean {
    switch (this.authMode) {
      case AuthMode.Login:
        this.checkUsername();
        if (this.login.username == "" || !isNullOrEmpty(this.validations.loginUsername)) {
          return false;
        }

        if (this.login.password == "") {
          return false;
        }

        break;

      case AuthMode.Register:
        this.checkUsername();
        this.checkPassword();
        this.checkFirstname();
        this.checkLastname();
        this.checkEmail();
        this.checkPhonenumber();

        if (this.register.username == "" || !isNullOrEmpty(this.validations.registerUsername)) {
          return false;
        }

        if (this.register.password == "" || !isNullOrEmpty(this.validations.registerPassword)) {
          return false;
        }

        if (this.register.firstname == "" || !isNullOrEmpty(this.validations.firstname)) {
          return false;
        }

        if (this.register.lastname == "" || !isNullOrEmpty(this.validations.lastname)) {
          return false;
        }

        if (this.register.email == "" || !isNullOrEmpty(this.validations.email)) {
          return false;
        }

        if (this.register.phoneNumber == "" || !isNullOrEmpty(this.validations.phoneNumber)) {
          return false;
        }
        break;

    }

    return true;
  }

  async authorizeAction(): Promise<void> {
    if (!this.ValidateForm()) {
      notifyError("", "Please enter all fields correctly");
    }

    if (this.authMode == AuthMode.Login) {
      await userModule.login(this.login);

      if (userModule?.currentUser?.roles.includes("Admin")) {
        // router.push({ name: routesNames.userPanel, path: "userPanel" });
        window.location.href = "userPanel";
      }
      else {
        // router.push({ name: routesNames.userPanel });
        window.location.href = "userPanel";
      }
      notifySuccess("Successfully Logged in");
    }

    if (this.authMode == AuthMode.Register) {
      await userModule.register(this.register);
      router.push({ name: routesNames.userPanel });
      notifySuccess("Welcome to Saloot Project");
    }
  }
}
</script>

<style scoped>
.validation {
  color: red;
}
</style>