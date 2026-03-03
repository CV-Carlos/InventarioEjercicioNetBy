import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MensajeService } from '../../../core/services';

@Component({
  selector: 'app-alerta',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './alerta.component.html',
})
export class AlertaComponent {
  mensajeService = inject(MensajeService);
}
