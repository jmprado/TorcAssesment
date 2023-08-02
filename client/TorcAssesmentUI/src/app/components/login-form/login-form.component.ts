import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { LoginService } from '../../services/login-service';
import { LoginUser } from '../../models/loginUser';
import { GlobalService } from '../../services/global-service';


@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent implements OnInit {
  form!: FormGroup;

  constructor(private formBuilder: FormBuilder, private loginService: LoginService, private globalService: GlobalService) { }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  submit() {
    if (this.form?.valid) {
      const loginUser = new LoginUser(this.form.value);
      if (loginUser) {
        this.loginService.loginUser(loginUser).then(response => {
          const data = response.data;
          this.globalService.loggedUser.next({
            id: data.id,
            username: data.username,
            role: data.role,
            token: data.token,
            isLogged: true
          });

          this.error = '';

          console.log(JSON.stringify(this.globalService.loggedUser.value));
        }).catch(error => {
          const reponseStatus = error.response.request.status;
          if (reponseStatus === 401) {
            this.error = "Unauthorized";
          }
          else {
            this.error = "Ops, something bad happened in this code.";
            console.log(error);
          }
        });
      }
    }
  }

  @Input() error: string | null = null;

  @Output() submitEM = new EventEmitter();
}
