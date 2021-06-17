<template>
  <div>
    <div class="row justify-content-center">
      <!-- left column -->
      <div class="col-md-6">
        <!-- general form elements -->
        <div class="card card-primary">
          <div class="card-header">
            <h3 class="card-title">Change your information</h3>
          </div>
          <!-- /.card-header -->
          <!-- form start -->
          <form>
            <div class="card-body">
              <div class="form-row">
                <div class="col form-group">
                  <label>Username </label>
                  <input
                    type="text"
                    v-model="userUpdate.username"
                    @change="checkUsername()"
                    class="form-control"
                    placeholder="Enter new Username"
                  />
                  <small class="validation">{{ validations.username }}</small>
                </div>

                <div class="col form-group">
                  <label>Profile Pricture </label>
                  <div class="custom-file">
                    <input
                      id="customFile"
                      type="file"
                      class="custom-file-input"
                      v-on:change="handleFileUpload($event)"
                      accept="image/*"
                    />
                    <label class="custom-file-label" for="customFile">{{
                      lblFilename
                    }}</label>
                  </div>
                </div>
              </div>

              <div class="form-row">
                <div class="col form-group">
                  <label>Firstname </label>
                  <input
                    type="text"
                    v-model="userUpdate.firstname"
                    @change="checkFirstname()"
                    class="form-control"
                    placeholder="Enter your Firstname"
                  />
                  <small class="validation">{{ validations.firstname }}</small>
                </div>

                <div class="col form-group">
                  <label>Lastname</label>
                  <input
                    type="text"
                    v-model="userUpdate.lastname"
                    @change="checkLastname()"
                    class="form-control"
                    placeholder="Enter your Lastname"
                  />
                  <small class="validation">{{ validations.lastname }}</small>
                </div>
              </div>

              <div class="form-row">
                <div class="col form-group">
                  <label>Email </label>
                  <input
                    type="text"
                    v-model="userUpdate.email"
                    @change="checkEmail()"
                    class="form-control"
                    placeholder="Enter your Email address"
                  />
                  <small class="validation">{{ validations.email }}</small>
                </div>

                <div class="col form-group">
                  <label>Phone Number</label>
                  <input
                    type="text"
                    v-model="userUpdate.phoneNumber"
                    @change="checkPhonenumber()"
                    class="form-control"
                    placeholder="Enter your Phone number"
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
                    v-model="userUpdate.birthdate"
                    class="form-control"
                    placeholder=""
                  />
                </div>

                <div class="col form-group">
                  <label>Gender</label>
                  <div class="form-control">
                    <div class="form-check form-check-inline">
                      <label class="form-check-label pr-2" for="inlineRadioMale"
                        >Male</label
                      >
                      <input
                        id="inlineRadioMale"
                        type="radio"
                        name="gender"
                        value="1"
                        v-model="userUpdate.gender"
                        class="form-check-input"
                        checked
                      />
                    </div>

                    <div class="form-check form-check-inline">
                      <label
                        class="form-check-label pl-3 pr-2"
                        for="inlineRadioFemale"
                        >Female</label
                      >
                      <input
                        id="inlineRadioFemale"
                        type="radio"
                        name="gender"
                        value="2"
                        v-model="userUpdate.gender"
                        class="form-check-input"
                      />
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <!-- /.card-body -->

            <div class="card-footer">
              <button type="button" class="btn btn-primary" @click="save()">
                Save
              </button>
            </div>
          </form>
        </div>
        <!-- /.card -->
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { UserUpdate } from "@/models/user/user";
import userModule from "@/store/modules/user/userModule";
import validationUtils from "@/common/validationUtils";
import { notifySuccess, notifyError } from "@/common/notificationUtils";
import GenderType from "@/common/Enums/genderType";
import { isNullOrEmpty } from "@/common/stringUtils";
import AccountSettingStoreModel from "@/models/user/accountSettingStoreModel";

@Component
export default class AccountSetting extends Vue {

  private lblFilename = "Choose file";
  private userUpdate: UserUpdate = {
    username: userModule.currentUser?.username as string,
    firstname: userModule.currentUser?.firstname as string,
    lastname: userModule.currentUser?.lastname as string,
    email: userModule.currentUser?.email as string,
    birthdate: userModule.currentUser?.birthdate as string,
    phoneNumber: userModule.currentUser?.phoneNumber as string,
    gender: userModule.currentUser?.gender as GenderType,
    teamId: userModule.currentUser?.teamId as number,
    profilePictureId: userModule.currentUser?.profilePictureId as number,
  };

  private accountSettingStoreModel: AccountSettingStoreModel = {
    userId: userModule.currentUser?.id as number,
    userUpdate: this.userUpdate,
    profilePicture: null
  };

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  private validations: any = {
    username: "",
    password: "",
    firstname: "",
    lastname: "",
    email: "",
    phoneNumber: "",
  };

  checkUsername(): void {
    if (!validationUtils.isValidUsername(this.userUpdate?.username)) {
      this.validations.username = "Username is invalid";
    }
    else if (this.userUpdate?.username?.length == 0) {
      this.validations.username = "Username is invalid";
    }
    else if (this.userUpdate?.username?.length > 15) {
      this.validations.username = "Username should be less than 15 characters";
    }
    else {
      this.validations.username = "";
    }
  }

  checkEmail(): void {
    if (!validationUtils.isValidEmail(this.userUpdate?.email)) {
      this.validations.email = "Email is invalid";
    } else {
      this.validations.email = "";
    }
  }

  checkFirstname(): void {
    if (this.userUpdate?.firstname?.length == 0) {
      this.validations.firstname = "Firstname is invalid";
    }
    else if (this.userUpdate.firstname.length > 35) {
      this.validations.firstname = "Firstname should be less than 35 characters";
    }
    else {
      this.validations.firstname = "";
    }
  }

  checkLastname(): void {
    if (this.userUpdate?.lastname?.length == 0) {
      this.validations.lastname = "Lastname is invalid";
    }
    else if (this.userUpdate.firstname.length > 35) {
      this.validations.lastname = "Lastname should be less than 35 characters";
    }
    else {
      this.validations.lastname = "";
    }
  }

  checkPhonenumber(): void {
    if (this.userUpdate?.phoneNumber?.length == 0) {
      this.validations.phoneNumber = "Phone Number is invalid";
    }
    else {
      this.validations.phoneNumber = "";
    }
  }

  ValidateForm(): boolean {
    this.checkUsername();
    this.checkFirstname();
    this.checkLastname();
    this.checkEmail();
    this.checkPhonenumber();

    if (this.userUpdate.username == "" || !isNullOrEmpty(this.validations.userUsername)) {
      return false;
    }

    if (this.userUpdate.firstname == "" || !isNullOrEmpty(this.validations.firstname)) {
      return false;
    }

    if (this.userUpdate.lastname == "" || !isNullOrEmpty(this.validations.lastname)) {
      return false;
    }

    if (this.userUpdate.email == "" || !isNullOrEmpty(this.validations.email)) {
      return false;
    }

    if (this.userUpdate.phoneNumber == "" || !isNullOrEmpty(this.validations.phoneNumber)) {
      return false;
    }
    return true;

  }

  // eslint-disable-next-line @typescript-eslint/explicit-module-boundary-types
  handleFileUpload(event: any): void {
    this.accountSettingStoreModel.profilePicture = event.target.files[0];
    this.lblFilename = this.accountSettingStoreModel.profilePicture.name;
  }

  async save(): Promise<void> {
    if (!this.ValidateForm()) {
      notifyError("", "Please enter all fields correctly");
    }
    else {
      await userModule.update(this.accountSettingStoreModel);
      notifySuccess("Your profile updated");
    }
  }

}
</script>

<style scoped>
.validation {
  color: red;
}
</style>